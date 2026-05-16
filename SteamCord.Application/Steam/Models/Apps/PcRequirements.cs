using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class PcRequirements
{
    [JsonProperty("minimum")]
    public string Minimum { get; set; } = null!;
}