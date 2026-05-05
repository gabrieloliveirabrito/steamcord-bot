using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces;

namespace SteamCord.Infrastructure.Services;

public class SteamService(HttpClient http, AppSettings appSettings) : ISteamService
{
    public Task<string?> GetGameBanner(int appId)
    {
        return Task.FromResult<string?>($"https://cdn.cloudflare.steamstatic.com/steam/apps/{appId}/library_hero.jpg");
    }

    public Task<string?> GetGameHeader(int appId)
    {
        return Task.FromResult<string?>($"https://cdn.cloudflare.steamstatic.com/steam/apps/{appId}/library_hero.jpg");
    }
}