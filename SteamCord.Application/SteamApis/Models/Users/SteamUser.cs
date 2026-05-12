using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SteamCord.Application.SteamApis.Models.Users;

public sealed class SteamUser
{
    [JsonProperty("steamID")]
    public string SteamId { get; set; } = default!;

    [JsonProperty("avatar")]
    public SteamAvatar Avatar { get; set; } = default!;

    [JsonProperty("url")]
    public string Url { get; set; } = default!;

    [JsonProperty("visible")]
    public bool Visible { get; set; }

    [JsonProperty("personaState")]
    public SteamPersonageState PersonaState { get; set; }    

    [JsonProperty("personaStateFlags")]
    public int? PersonaStateFlags { get; set; }

    [JsonProperty("allowsComments")]
    public bool AllowsComments { get; set; }

    [JsonProperty("nickname")]
    public string DisplayName { get; set; } = default!;

    [JsonProperty("lastLogOffTimestamp")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? LastLogOff { get; set; }

    [JsonProperty("createdTimestamp")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("primaryGroupID")]
    public string? PrimaryGroupID { get; set; }

    [JsonProperty("countryCode")]
    public string? CountryCode { get; set; } = default!;

    [JsonProperty("stateCode")]
    public string? StateCode { get; set; } = default!;

    [JsonProperty("cityID")]
    public int? CityID { get; set; }
    
    [JsonProperty("gameID")]
    public string? GameID { get; set; }

    [JsonProperty("gameName")]
    public string? GameName { get; set; }
}