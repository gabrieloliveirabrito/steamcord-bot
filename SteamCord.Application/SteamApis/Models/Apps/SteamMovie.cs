using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamMovie
{
    [JsonProperty("id")]
    public long Id { get; init; }

    [JsonProperty("name")]
    public string? Name { get; init; }

    [JsonProperty("thumbnail")]
    public string? Thumbnail { get; init; }

    [JsonProperty("highlight")]
    public bool Highlight { get; init; }
}