using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed class SteamAvatar
{
    [JsonProperty("small")]
    public string Small { get; set; } = default!;

    [JsonProperty("medium")]
    public string Medium { get; set; } = default!;

    [JsonProperty("large")]
    public string Large { get; set; } = default!;

    [JsonProperty("hash")]
    public string Hash { get; set; } = default!;

    public string? Fetch()
    {
        if (!string.IsNullOrEmpty(Large)) return Large;
        if (!string.IsNullOrEmpty(Medium)) return Medium;
        if (!string.IsNullOrEmpty(Small)) return Small;

        return null;
    }
}