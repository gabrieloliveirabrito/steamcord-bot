using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamReleaseDate
{
    [JsonProperty("comingSoon")]
    public bool ComingSoon { get; init; }

    [JsonProperty("date")]
    public string? Date { get; init; }
}