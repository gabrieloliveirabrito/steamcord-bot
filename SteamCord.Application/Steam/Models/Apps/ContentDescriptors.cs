using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class ContentDescriptors
{
    [JsonProperty("ids")]
    public List<int> Ids { get; set; } = [];

    [JsonProperty("notes")]
    public string? Notes { get; set; }
}