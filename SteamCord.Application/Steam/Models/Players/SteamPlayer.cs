using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models.Players;

public class SteamPlayer
{
    [JsonProperty("steamid")]
    public string Id { get; set; }  = default!;

    [JsonProperty("communityvisibilitystate")]
    public SteamVisibilityState State { get; set; }

    [JsonProperty("profilestate")]
    public int ProfileState { get; set; }

    [JsonProperty("personaname")]
    public string Name { get; set; } = default!;

    [JsonProperty("profileurl")]
    public string ProfileUrl { get; set; } = default!;

    [JsonProperty("avatar")]
    public string Avatar { get; set; } = default!;

    [JsonProperty("avatarmedium")]
    public string AvatarMedium { get; set; } = default!;

    [JsonProperty("avatarfull")]
    public string AvatarFull { get; set; } = default!;

    [JsonProperty("avatarhash")]
    public string AvatarHash { get; set; } = default!;

    [JsonProperty("lastlogoff")]
    public long LastLogOff { get; set; }

    [JsonProperty("personastate")]
    public SteamPersonaState PersonaState { get; set; }

    [JsonProperty("realname")]
    public string? RealName { get; set; } = default!;

    [JsonProperty("primaryclanid")]
    public string? PrimaryClanID { get; set; }

    [JsonProperty("timecreated")]
    public long TimeCreated { get; set; }

    [JsonProperty("personastateflags")]
    public int PersonaStateFlags { get; set; }

    [JsonProperty("loccountrycode")]
    public string? CountryCode { get; set; }

    [JsonProperty("locstatecode")]
    public string? StateCode { get; set; }

    [JsonProperty("loccityid")]
    public string? CityID { get; set; }

    [JsonProperty("commentpermission")]
    public int? CommentPermission { get; set; }

    [JsonProperty("gameid")]
    public string? GameID { get; set; }

    [JsonProperty("gameserverip")]
    public string? GameServerIP { get; set; }

    [JsonProperty("gameextrainfo")]
    public string? GameExtraInfo { get; set; }
}