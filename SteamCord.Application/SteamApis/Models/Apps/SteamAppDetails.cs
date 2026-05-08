using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamAppDetails
{
    [JsonProperty("_id")]
    public string? Id { get; init; }

    [JsonProperty("appId")]
    public int AppId { get; init; }

    [JsonProperty("name")]
    public string? Name { get; init; }

    [JsonProperty("shortDescription")]
    public string? ShortDescription { get; init; }

    [JsonProperty("aboutTheGame")]
    public string? AboutTheGame { get; init; }

    [JsonProperty("detailedDescription")]
    public string? DetailedDescription { get; init; }

    [JsonProperty("headerImage")]
    public string? HeaderImage { get; init; }

    [JsonProperty("capsuleImage")]
    public string? CapsuleImage { get; init; }

    [JsonProperty("background")]
    public string? Background { get; init; }

    [JsonProperty("website")]
    public string? Website { get; init; }

    [JsonProperty("type")]
    public string? Type { get; init; }

    [JsonProperty("isFree")]
    public bool IsFree { get; init; }

    [JsonProperty("isSuccess")]
    public bool IsSuccess { get; init; }

    [JsonProperty("failureReason")]
    public string? FailureReason { get; init; }

    [JsonProperty("developers")]
    public IReadOnlyList<string> Developers { get; init; } = [];

    [JsonProperty("publishers")]
    public IReadOnlyList<string> Publishers { get; init; } = [];

    [JsonProperty("genres")]
    public IReadOnlyList<SteamNamedItem> Genres { get; init; } = [];

    [JsonProperty("categories")]
    public IReadOnlyList<SteamCategory> Categories { get; init; } = [];

    [JsonProperty("platforms")]
    public SteamPlatforms? Platforms { get; init; }

    [JsonProperty("priceOverview")]
    public SteamPriceOverview? PriceOverview { get; init; }

    [JsonProperty("metacritic")]
    public SteamMetacritic? Metacritic { get; init; }

    [JsonProperty("recommendations")]
    public SteamRecommendations? Recommendations { get; init; }

    [JsonProperty("releaseDate")]
    public SteamReleaseDate? ReleaseDate { get; init; }

    [JsonProperty("achievements")]
    public SteamAchievements? Achievements { get; init; }

    [JsonProperty("screenshots")]
    public IReadOnlyList<SteamScreenshot> Screenshots { get; init; } = [];

    [JsonProperty("movies")]
    public IReadOnlyList<SteamMovie> Movies { get; init; } = [];

    [JsonProperty("ratings")]
    public Dictionary<string, SteamRating>? Ratings { get; init; }

    [JsonProperty("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; init; }

    [JsonProperty("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }
}