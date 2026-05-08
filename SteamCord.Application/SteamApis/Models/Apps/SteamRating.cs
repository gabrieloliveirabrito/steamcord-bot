using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamRating
{
    [JsonProperty("rating")]
    public string? Rating { get; init; }

    [JsonProperty("descriptors")]
    public string? Descriptors { get; init; }

    [JsonProperty("required_age")]
    public string? RequiredAge { get; init; }
}