using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class Category
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; } = null!;
}