using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class SupportInfo
{
    [JsonProperty("url")]
    public string Url { get; set; } = null!;

    [JsonProperty("email")]
    public string Email { get; set; } = null!;
}