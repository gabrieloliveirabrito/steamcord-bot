using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Players;

public class SteamSummaryResponse
{
    [JsonProperty("players")]
    public List<SteamPlayer> Players { get ; set; } = [];
}