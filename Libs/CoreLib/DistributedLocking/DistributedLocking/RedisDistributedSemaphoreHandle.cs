using DistributedLocking.Interfaces;
using DistributedLocking.Scripts;
using StackExchange.Redis;

namespace DistributedLocking;

/// <inheritdoc />
internal sealed class RedisDistributedSemaphoreHandle : IDistributedSynchronizationHandle
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _mux;
    private readonly string _holderKey;
    private readonly string _permitsKey;
    private readonly TimeSpan _holderTtl;
    private readonly TimeSpan _heartbeatInterval;
    private readonly CancellationTokenSource _handleLostCts = new();
    private readonly CancellationTokenSource _internalCts = new();
    private Task? _heartbeatTask;
    private ISubscriber? _subscriber;
    private string? _subscriptionChannel;
    private bool _disposed;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RedisDistributedSemaphoreHandle(
        IConnectionMultiplexer mux,
        IDatabase db,
        string holderKey,
        string permitsKey,
        TimeSpan holderTtl,
        TimeSpan heartbeatInterval)
    {
        _mux = mux ?? throw new ArgumentNullException(nameof(mux));
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _holderKey = holderKey ?? throw new ArgumentNullException(nameof(holderKey));
        _permitsKey = permitsKey ?? throw new ArgumentNullException(nameof(permitsKey));
        _holderTtl = holderTtl;
        _heartbeatInterval = heartbeatInterval;

        _mux.ConnectionFailed += OnConnectionFailed;
        _mux.ConnectionRestored += OnConnectionRestored;
    }

    public CancellationToken HandleLostToken => _handleLostCts.Token;

    /// <summary>
    /// Запускает heartbeat и подписку на keyspace notifications для отслеживания потери семафора.
    /// </summary>
    public void StartHeartbeatAndWatch()
    {
        _heartbeatTask = Task.Run(HeartbeatLoop);
        TrySubscribeKeyspaceNotification();
    }

    /// <summary>
    /// Подписка на keyspace notifications Redis, чтобы отследить удаление или истечение ключа.
    /// </summary>
    private void TrySubscribeKeyspaceNotification()
    {
        try
        {
            _subscriber = _mux.GetSubscriber();
            var dbNumber = _db.Database;
            _subscriptionChannel = $"__keyspace@{dbNumber}__:{_holderKey}";

            _subscriber.Subscribe(_subscriptionChannel, (ch, msg) =>
            {
                var evt = msg.ToString();
                if (evt == "expired" || evt == "del")
                {
                    TrySignalHandleLost();
                }
            });
        }
        catch
        {
        }
    }

    /// <summary>
    /// Цикл heartbeat, который обновляет TTL ключа держателя.
    /// Если обновление не удалось — сигнализируем потерю семафора.
    /// </summary>
    private async Task HeartbeatLoop()
    {
        try
        {
            while (!_internalCts.IsCancellationRequested)
            {
                try
                {
                    var ok = await _db.KeyExpireAsync(_holderKey, _holderTtl).ConfigureAwait(false);
                    if (!ok)
                    {
                        TrySignalHandleLost();
                        return;
                    }
                }
                catch
                {
                    TrySignalHandleLost();
                    return;
                }

                await Task.Delay(_heartbeatInterval, _internalCts.Token).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    /// <summary>
    /// Срабатывает при потере соединения с Redis — считаем семафор потерянным.
    /// </summary>
    private void OnConnectionFailed(object? sender, ConnectionFailedEventArgs e)
    {
        TrySignalHandleLost();
    }

    /// <summary>
    /// Срабатывает при потере соединения с Redis — считаем семафор потерянным.
    /// </summary>
    private void OnConnectionRestored(object? sender, ConnectionFailedEventArgs e)
    {
    }

    /// <summary>
    /// Срабатывает при потере соединения с Redis — считаем семафор потерянным.
    /// </summary>
    private void TrySignalHandleLost()
    {
        if (!_handleLostCts.IsCancellationRequested)
        {
            try { _handleLostCts.Cancel(); } catch { }
        }
    }

    /// <summary>
    /// Асинхронно освобождает семафор, останавливает heartbeat и отписывается от уведомлений.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        _internalCts.Cancel();

        try
        {
            if (_heartbeatTask != null) 
                await _heartbeatTask.ConfigureAwait(false);
        }
        catch { }

        try
        {
            await _db.ScriptEvaluateAsync(
                RedisLuaScripts.Release,
                new RedisKey[] { _holderKey, _permitsKey },
                new RedisValue[] { } 
            ).ConfigureAwait(false);
        }
        catch { }

        TrySignalHandleLost();

        try
        {
            if (_subscriber != null && !string.IsNullOrEmpty(_subscriptionChannel))
            {
                _subscriber.Unsubscribe(_subscriptionChannel);
            }
        }
        catch { }

        _mux.ConnectionFailed -= OnConnectionFailed;
        _mux.ConnectionRestored -= OnConnectionRestored;

        _handleLostCts.Dispose();
        _internalCts.Dispose();
    }

    /// <summary>
    /// Синхронная версия Dispose.
    /// </summary>
    public void Dispose() => DisposeAsync().AsTask().GetAwaiter().GetResult();
}