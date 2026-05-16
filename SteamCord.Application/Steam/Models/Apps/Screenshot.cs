using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class Screenshot
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("path_thumbnail")]
    public string Thumbnail { get; set; } = null!;

    [JsonProperty("path_full")]
    public string Full { get; set; } = null!;
}