namespace DistributedLocking;

/// <summary>
/// Опции
/// </summary>
public sealed class RedisSemaphoreOptions
{
    public string Name { get; set; } = "default";
    public int MaxCount { get; set; } = 1;

    public int HolderTtlMs { get; set; } = 30000;
    public TimeSpan HeartbeatInterval { get; set; } = TimeSpan.FromSeconds(10);
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromMilliseconds(50);

    public string KeyPrefix { get; set; } = "semaphore:";
}

