using StackExchange.Redis;
using SteamCord.Application.Interfaces.Services;

namespace SteamCord.Infrastructure.Services;

public class RedisService(IConnectionMultiplexer redis) : IRedisService
{    
    private readonly IDatabase _db = redis.GetDatabase();

    public async Task<string?> GetAsync(string key)
    {
        var value = await _db.StringGetAsync(key);

        return value.HasValue ? value.ToString() : null;
    }

    public Task SetAsync(string key, string value, TimeSpan? expiry = null)
    => _db.StringSetAsync(key, value, expiry, When.Always);

    public Task RemoveAsync(string key)
    => _db.KeyDeleteAsync(key);
}