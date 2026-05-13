namespace SteamCord.Application.Entities;

public class User
{
    public int Id { get; set; }
    public ulong DiscordId { get; set; }
    public string SteamId { get; set; } = null!;
    public string? LastGameId { get; set; }
    public string? LastGameName { get; set; }
    public DateTime? LastSeenAt { get; set; }

    public ICollection<UserGuild> UserGuilds { get; set; } = [];
}