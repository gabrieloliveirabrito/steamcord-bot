namespace SteamCord.Application.Interfaces.Services;

public interface IRedisService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, string value, TimeSpan? expiry = null);
    Task RemoveAsync(string key);
}