using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Apps;

public class SteamGameData
{
    [JsonProperty("type")]
    public string Type { get; set; } = null!;

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("steam_appid")]
    public int SteamAppId { get; set; }

    [JsonProperty("required_age")]
    public int RequiredAge { get; set; }

    [JsonProperty("is_free")]
    public bool IsFree { get; set; }

    [JsonProperty("detailed_description")]
    public string DetailedDescription { get; set; } = null!;

    [JsonProperty("about_the_game")]
    public string AboutTheGame { get; set; } = null!;

    [JsonProperty("short_description")]
    public string ShortDescription { get; set; } = null!;

    [JsonProperty("supported_languages")]
    public string SupportedLanguages { get; set; } = null!;

    [JsonProperty("header_image")]
    public string HeaderImage { get; set; } = null!;

    [JsonProperty("capsule_image")]
    public string CapsuleImage { get; set; } = null!;

    [JsonProperty("capsule_imagev5")]
    public string CapsuleImageV5 { get; set; } = null!;

    [JsonProperty("website")]
    public string? Website { get; set; }

    [JsonProperty("developers")]
    public List<string> Developers { get; set; } = null!;

    [JsonProperty("package_groups")]
    public List<object> PackageGroups { get; set; } = null!;

    [JsonProperty("platforms")]
    public Platforms Platforms { get; set; } = null!;

    [JsonProperty("categories")]
    public List<Category> Categories { get; set; } = null!;

    [JsonProperty("genres")]
    public List<Genre> Genres { get; set; } = null!;

    [JsonProperty("screenshots")]
    public List<Screenshot> Screenshots { get; set; } = null!;

    [JsonProperty("recommendations")]
    public Recommendations Recommendations { get; set; } = null!;

    [JsonProperty("release_date")]
    public ReleaseDate ReleaseDate { get; set; } = null!;

    [JsonProperty("support_info")]
    public SupportInfo SupportInfo { get; set; } = null!;

    [JsonProperty("background")]
    public string Background { get; set; } = null!;

    [JsonProperty("background_raw")]
    public string BackgroundRaw { get; set; } = null!;

    [JsonProperty("content_descriptors")]
    public ContentDescriptors ContentDescriptors { get; set; } = null!;

    [JsonProperty("ratings")]
    public Ratings Ratings { get; set; } = null!;
}