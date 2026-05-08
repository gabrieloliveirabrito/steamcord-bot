using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamCategory
{
    [JsonProperty("id")]
    public int Id { get; init; }

    [JsonProperty("description")]
    public string? Description { get; init; }
}