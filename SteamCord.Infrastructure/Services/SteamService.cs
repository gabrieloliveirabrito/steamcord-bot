using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces.Services;
using SteamCord.Application.Steam.Models;
using SteamCord.Application.Steam.Models.Players;

namespace SteamCord.Infrastructure.Services;

public class SteamService(ILogger<SteamService> logger, SteamSettings steamSettings, HttpClient http) : ISteamService
{
    public async Task<IReadOnlyList<SteamPlayer>?> GetPlayerSummaries(IEnumerable<string> steamIds, CancellationToken ct = default)
    {
        var ids = string.Join(",", steamIds);
        logger.LogInformation($"ISteamUser/GetPlayerSummaries/v2/?key={steamSettings.ApiKey}&steamids={ids}");

        var response = await http.GetAsync(
            $"ISteamUser/GetPlayerSummaries/v2/?key={steamSettings.ApiKey}&steamids={ids}",
            ct
        );

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to get PlayerSummaries on SteamAPI!");
            return null;
        }

        var json = await response.Content.ReadAsStringAsync(ct);
        logger.LogInformation(json);
        var result = JsonConvert.DeserializeObject<BaseResponse<SteamSummaryResponse>>(json);

        return result?.Response?.Players;
    }
}