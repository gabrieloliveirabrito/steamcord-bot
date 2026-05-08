using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamPlatforms
{
    [JsonProperty("windows")]
    public bool Windows { get; init; }

    [JsonProperty("mac")]
    public bool Mac { get; init; }

    [JsonProperty("linux")]
    public bool Linux { get; init; }
}