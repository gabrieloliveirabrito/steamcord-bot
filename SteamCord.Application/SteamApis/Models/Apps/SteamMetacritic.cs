using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamMetacritic
{
    [JsonProperty("score")]
    public int? Score { get; init; }

    [JsonProperty("url")]
    public string? Url { get; init; }
}