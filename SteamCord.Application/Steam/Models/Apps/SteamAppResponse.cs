using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class SteamAppResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("data")]
    public SteamGameData Data { get; set; } = null!;
}