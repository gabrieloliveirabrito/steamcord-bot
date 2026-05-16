using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class ReleaseDate
{
    [JsonProperty("coming_soon")]
    public bool ComingSoon { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; } = null!;
}