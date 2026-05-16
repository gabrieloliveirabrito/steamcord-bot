using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class Platforms
{
    [JsonProperty("windows")]
    public bool Windows { get; set; }

    [JsonProperty("mac")]
    public bool Mac { get; set; }

    [JsonProperty("linux")]
    public bool Linux { get; set; }
}