using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces;
using SteamCord.Application.SteamApis.Models;
using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Infrastructure.Services;

public class SteamApisService(ILogger<SteamApisService> logger, HttpClient http, IRedisService redis) : ISteamApisService
{
    private async Task<T?> MakeRequest<T>(string url, string redisKey, Action<HttpRequestMessage>? builder = null, CancellationToken ct = default)
    {
        var existsKey = $"{redisKey}:exists";
        try
        {
            var existsValue = await redis.GetAsync(existsKey);
            if (!string.IsNullOrEmpty(existsValue))
            {
                logger.LogWarning("{key} skipped due cached failure ({content})", redisKey, existsValue);
                return default;
            }

            var redisValue = await redis.GetAsync(redisKey);
            if (!string.IsNullOrEmpty(redisValue))
            {
                return JsonConvert.DeserializeObject<T>(redisValue);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            builder?.Invoke(request);

            var response = await http.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode)
            {
                await redis.SetAsync(existsKey, $"Request {url} returned {response.StatusCode}", TimeSpan.FromMinutes(10));
                logger.LogError("Failed to send the request to {url}", url);
                return default;
            }

            var json = await response.Content.ReadAsStringAsync(ct);
            var apiResponse = JsonConvert.DeserializeObject<BaseResponse<T>>(json);

            if (apiResponse?.Success is true)
            {
                await redis.SetAsync(redisKey, JsonConvert.SerializeObject(apiResponse.Result), TimeSpan.FromDays(30));
                return apiResponse.Result;
            }
            else
            {
                await redis.SetAsync(existsKey, $"Request {url} failed on SteamApis", TimeSpan.FromMinutes(10));
                return default;
            }
        }
        catch (Exception ex)
        {
            await redis.SetAsync(existsKey, $"Request {url} exception: {ex.Message}", TimeSpan.FromMinutes(10));

            logger.LogCritical(ex, "Critical error on SteamService request to {0}", url);
            return default;
        }
    }

    public Task<SteamUser?> GetSteamUserAsync(string steamId, CancellationToken ct = default)
        => MakeRequest<SteamUser>($"users/{steamId}", $"steam:users:{steamId}", ct: ct);

    public Task<SteamAppDetails?> GetSteamAppDetailsAsync(string appId, CancellationToken ct = default)
        => MakeRequest<SteamAppDetails>($"apps/{appId}", $"steam:apps:{appId}", ct: ct);
}