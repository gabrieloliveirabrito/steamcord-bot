using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces;
using SteamCord.Application.SteamApis.Models;
using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Infrastructure.Services;

public class SteamService(ILogger<SteamService> logger, HttpClient http, AppSettings appSettings) : ISteamService
{
    public Task<string?> GetGameBanner(int appId)
    {
        return Task.FromResult<string?>($"https://cdn.cloudflare.steamstatic.com/steam/apps/{appId}/library_hero.jpg");
    }

    public Task<string?> GetGameHeader(int appId)
    {
        return Task.FromResult<string?>($"https://cdn.cloudflare.steamstatic.com/steam/apps/{appId}/library_hero.jpg");
    }

    private async Task<T?> MakeRequest<T>(string url, Action<HttpRequestMessage> builder = null, CancellationToken ct = default)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            builder?.Invoke(request);

            var response = await http.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Failed to send the request to {url}", url);
                return default;
            }

            var json = await response.Content.ReadAsStringAsync(ct);
            var apiResponse = JsonConvert.DeserializeObject<BaseResponse<T>>(json);

            return apiResponse?.Success is true ? apiResponse.Result : default;
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Critical error on SteamService request to {0}", url);
            return default;
        }
    }

    public Task<SteamUser?> GetSteamUserAsync(string steamId, CancellationToken ct = default)
        => MakeRequest<SteamUser>($"users/{steamId}", ct: ct);

    public Task<SteamAppDetails?> GetSteamAppDetailsAsync(string appId, CancellationToken ct = default)
        => MakeRequest<SteamAppDetails>($"apps/{appId}", ct: ct);
}