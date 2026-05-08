using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamAchievements
{
    [JsonProperty("total")]
    public int Total { get; init; }

    [JsonProperty("highlighted")]
    public IReadOnlyList<SteamAchievement> Highlighted { get; init; } = [];
}