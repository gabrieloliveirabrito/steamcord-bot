using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class Recommendations
{
    [JsonProperty("total")]
    public int Total { get; set; }
}