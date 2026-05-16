using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class EsrbRating
{
    [JsonProperty("rating")]
    public string Rating { get; set; } = null!;
}