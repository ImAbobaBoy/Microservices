namespace DistributedLocking.Scripts
{
    /// <summary>
    /// Луа скрипты для семафора на редисе
    /// </summary>
    public static class RedisLuaScripts
    {
        // KEYS[1] = permitsKey, KEYS[2] = holderKey
        // ARGV[1] = maxPermits, ARGV[2] = ttlMs
        public const string Acquire = @"
-- safe semaphore acquire
local permits_key = KEYS[1]
local holder_key = KEYS[2]
local max_permits = tonumber(ARGV[1]) or 0
local ttl_ms = tonumber(ARGV[2]) or 0

-- read current value (may be nil/false)
local current = redis.call('GET', permits_key)
local permits

if current == false or current == nil then
    -- initialize if absent
    permits = max_permits
    -- store as string
    redis.call('SET', permits_key, tostring(permits))
else
    permits = tonumber(current)
    if permits == nil then
        -- defensive fallback: if value is non-numeric, reset
        permits = max_permits
        redis.call('SET', permits_key, tostring(permits))
    end
end

-- now permits is a number
if permits <= 0 then
    return 0
end

-- consume one permit and create holder key with TTL
redis.call('DECR', permits_key)
redis.call('SET', holder_key, '1', 'PX', ttl_ms)
return 1
";

        // KEYS[1] = holderKey, KEYS[2] = permitsKey
        public const string Release = @"
local holder_key = KEYS[1]
local permits_key = KEYS[2]

local exists = redis.call('GET', holder_key)
if exists and exists ~= false then
    redis.call('DEL', holder_key)
    redis.call('INCR', permits_key)
    return 1
end

return 0
";
    }
}