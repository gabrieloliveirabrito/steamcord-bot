using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamScreenshot
{
    [JsonProperty("id")]
    public int Id { get; init; }

    [JsonProperty("pathThumbnail")]
    public string? PathThumbnail { get; init; }

    [JsonProperty("pathFull")]
    public string? PathFull { get; init; }
}