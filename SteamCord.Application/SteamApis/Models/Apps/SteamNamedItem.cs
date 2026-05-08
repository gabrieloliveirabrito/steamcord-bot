using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamNamedItem
{
    [JsonProperty("id")]
    public string? Id { get; init; }

    [JsonProperty("description")]
    public string? Description { get; init; }
}