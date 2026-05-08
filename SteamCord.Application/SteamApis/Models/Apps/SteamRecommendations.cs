using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamRecommendations
{
    [JsonProperty("total")]
    public int Total { get; init; }
}