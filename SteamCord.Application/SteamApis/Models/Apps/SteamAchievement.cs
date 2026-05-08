using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamAchievement
{
    [JsonProperty("name")]
    public string? Name { get; init; }

    [JsonProperty("path")]
    public string? Path { get; init; }
}