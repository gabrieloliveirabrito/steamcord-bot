using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class Genre
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;

    [JsonProperty("description")]
    public string Description { get; set; } = null!;
}