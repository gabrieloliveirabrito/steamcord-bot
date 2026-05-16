using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces.Services;
using SteamCord.Application.Steam.Models;
using SteamCord.Application.Steam.Models.Apps;
using SteamCord.Application.Steam.Models.Players;

namespace SteamCord.Infrastructure.Services;

public class SteamService(ILogger<SteamService> logger, SteamSettings steamSettings, HttpClient http) : ISteamService
{
    public async Task<Dictionary<string, SteamAppResponse>> GetAppsData(IEnumerable<string> appIds, CancellationToken ct = default)
    {
        var ids = string.Join(",", appIds);
        logger.LogInformation($"https://store.steampowered.com/api/appdetails?appids={ids}");
        
        var response = await http.GetAsync($"https://store.steampowered.com/api/appdetails?appids={ids}", ct);

        var json = await response.Content.ReadAsStringAsync(ct);
        logger.LogInformation(json);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Failed to get apps data {0}", ids);
            return [];
        }

        var result = JsonConvert.DeserializeObject<Dictionary<string, SteamAppResponse>>(json);
        return result ?? [];
    }

    public async Task<IReadOnlyList<SteamPlayer>?> GetPlayerSummaries(IEnumerable<string> steamIds, CancellationToken ct = default)
    {
        var ids = string.Join(",", steamIds);

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

        //MOCK?
        //var json = "{\"response\":{\"players\":[{\"steamid\":\"76561198096847711\",\"gameid\":\"3357650\",\"gameextrainfo\":\"Unreal Gold\",\"communityvisibilitystate\":3,\"profilestate\":1,\"personaname\":\"tDark_GabrielOB\",\"profileurl\":\"https://steamcommunity.com/id/tDark_GabrielOB/\",\"avatar\":\"https://avatars.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230.jpg\",\"avatarmedium\":\"https://avatars.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230_medium.jpg\",\"avatarfull\":\"https://avatars.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230_full.jpg\",\"avatarhash\":\"5e7947e664e7a507e8a52dc563b8647519a05230\",\"lastlogoff\":1778897410,\"personastate\":1,\"realname\":\"Gabriel Oliveira Brito\",\"primaryclanid\":\"103582791433937404\",\"timecreated\":1373133959,\"personastateflags\":0,\"loccountrycode\":\"BR\",\"locstatecode\":\"15\",\"loccityid\":8456}]}}";
        //var json = "{\"response\":{\"players\":[{\"steamid\":\"76561198096847711\",\"communityvisibilitystate\":3,\"profilestate\":1,\"personaname\":\"tDark_GabrielOB\",\"profileurl\":\"https://steamcommunity.com/id/tDark_GabrielOB/\",\"avatar\":\"https://avatars.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230.jpg\",\"avatarmedium\":\"https://avatars.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230_medium.jpg\",\"avatarfull\":\"https://avatars.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230_full.jpg\",\"avatarhash\":\"5e7947e664e7a507e8a52dc563b8647519a05230\",\"lastlogoff\":1778897410,\"personastate\":1,\"realname\":\"Gabriel Oliveira Brito\",\"primaryclanid\":\"103582791433937404\",\"timecreated\":1373133959,\"personastateflags\":0,\"loccountrycode\":\"BR\",\"locstatecode\":\"15\",\"loccityid\":8456}]}}";
        //var result = JsonConvert.DeserializeObject<BaseResponse<SteamSummaryResponse>>(json);

        //return result?.Response?.Players;
    }
}