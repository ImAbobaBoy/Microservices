using DistributedLocking;
using DistributedLocking.Interfaces;
using Logic.Services.SemaphoreService.Interfaces;
using StackExchange.Redis;

namespace Logic.Services.SemaphoreService;

/// <inheritdoc />
public class SemaphoreService : ISemaphoreService
{
    private readonly IConnectionMultiplexer _mux;
    private readonly IDistributedSemaphore  _semaphore;

    /// <summary>
    /// Конструктор
    /// </summary>
    public SemaphoreService()
    {
        _mux = ConnectionMultiplexer.Connect("localhost:6379");
        if (!_mux.IsConnected)
        {
            Console.WriteLine("[ERROR] ConnectionMultiplexer failed to connect to Redis.");
            throw new InvalidOperationException("Redis not connected");
        }
        Console.WriteLine("[INFO] Connected Redis endpoints: ");
        
        var options = new RedisSemaphoreOptions
        {
            Name = "mySemaphore",
            MaxCount = 2,
            HolderTtlMs = 10000,
            HeartbeatInterval = TimeSpan.FromSeconds(2),
            RetryDelay = TimeSpan.FromMilliseconds(50)
        };

        _semaphore = new RedisDistributedSemaphore(_mux, options);
    }

    /// <inheritdoc />
    public IDistributedSemaphore GetSemaphore() => _semaphore;
}