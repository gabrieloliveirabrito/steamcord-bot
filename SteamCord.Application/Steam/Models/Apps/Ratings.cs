using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class Ratings
{
    [JsonProperty("esrb")]
    public EsrbRating Esrb { get; set; } = null!;
}