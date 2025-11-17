using System.Diagnostics;
using DistributedLocking.Interfaces;
using DistributedLocking.Scripts;
using StackExchange.Redis;

namespace DistributedLocking;

/// <inheritdoc />
public class RedisDistributedSemaphore : IDistributedSemaphore
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private readonly RedisSemaphoreOptions _options;

    public string Name => _options.Name;
    public int MaxCount => _options.MaxCount;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RedisDistributedSemaphore(
        IConnectionMultiplexer redis,
        RedisSemaphoreOptions options)
    {
        _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        _db = _redis.GetDatabase();
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public IDistributedSynchronizationHandle? TryAcquire(TimeSpan timeout = default, CancellationToken cancellationToken = default)
        => TryAcquireAsync(timeout, cancellationToken).AsTask().GetAwaiter().GetResult();

    /// <inheritdoc />
    public IDistributedSynchronizationHandle Acquire(TimeSpan? timeout = null, CancellationToken cancellationToken = default)
        => AcquireAsync(timeout, cancellationToken).AsTask().GetAwaiter().GetResult();

    /// <inheritdoc />
    public async ValueTask<IDistributedSynchronizationHandle?> TryAcquireAsync(TimeSpan timeout = default, CancellationToken cancellationToken = default)
    {
        var handle = await InternalAcquireAsync(timeout, throwOnTimeout: false, cancellationToken).ConfigureAwait(false);
        return handle;
    }

    /// <inheritdoc />
    public async ValueTask<IDistributedSynchronizationHandle> AcquireAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        var timeoutVal = timeout ?? Timeout.InfiniteTimeSpan;
        var handle = await InternalAcquireAsync(timeoutVal, throwOnTimeout: true, cancellationToken).ConfigureAwait(false);
        return handle!;
    }

    /// <summary>
    /// Внутренний метод захвата семафора
    /// </summary>
    private async Task<IDistributedSynchronizationHandle?> InternalAcquireAsync(
        TimeSpan timeout,
        bool throwOnTimeout,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        var infinite = timeout == Timeout.InfiniteTimeSpan;
        int attempt = 0;

        do
        {
            attempt++;
            cancellationToken.ThrowIfCancellationRequested();

            var id = Guid.NewGuid().ToString("N");
            var holderKey = $"{_options.KeyPrefix}{Name}:holder:{id}";
            var permitsKey = $"{_options.KeyPrefix}{Name}:permits";

            try
            {
                Debug.WriteLine($"[SEMA] Attempt #{attempt} - trying acquire (holder={holderKey}, permits={permitsKey})");

                // Вызов скрипта захвата
                var redisResult = await _db.ScriptEvaluateAsync(
                    RedisLuaScripts.Acquire,
                    new RedisKey[] { permitsKey, holderKey },
                    new RedisValue[] { _options.MaxCount, _options.HolderTtlMs }
                ).ConfigureAwait(false);

                string resultStr = redisResult.IsNull ? "<null>" : redisResult.ToString() ?? "<toString-null>";
                Debug.WriteLine($"[SEMA] Attempt #{attempt} - script returned raw: {resultStr} / Type: {redisResult.Type}");

                int intResult = 0;
                if (!redisResult.IsNull)
                {
                    if (redisResult.Type == ResultType.Integer)
                        intResult = (int)(long)redisResult;
                    else
                        int.TryParse(resultStr, out intResult);
                }

                Debug.WriteLine($"[SEMA] Attempt #{attempt} - parsed intResult = {intResult}");

                if (intResult == 1)
                {
                    Debug.WriteLine($"[SEMA] Attempt #{attempt} - ACQUIRED (holder={holderKey}).");
                    var handle = new RedisDistributedSemaphoreHandle(
                        _redis,
                        _db,
                        holderKey,
                        permitsKey,
                        TimeSpan.FromMilliseconds(_options.HolderTtlMs),
                        _options.HeartbeatInterval
                    );

                    handle.StartHeartbeatAndWatch();
                    return handle;
                }

                Debug.WriteLine($"[SEMA] Attempt #{attempt} - NOT acquired (script returned 0).");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[SEMA][ERROR] Attempt #{attempt} - ScriptEvaluateAsync threw: {ex.GetType().Name}: {ex.Message}");
            }

            if (!infinite && sw.Elapsed >= timeout) break;

            try
            {
                await Task.Delay(_options.RetryDelay, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { break; }
        }
        while (infinite || sw.Elapsed < timeout);

        if (throwOnTimeout)
        {
            Debug.WriteLine($"[SEMA] Timeout acquiring semaphore '{Name}' after {sw.Elapsed} (attempts={Math.Max(1, attempt)})");
            throw new TimeoutException($"Timeout acquiring semaphore '{Name}'");
        }

        return null;
    }
}