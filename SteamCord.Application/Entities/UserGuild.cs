namespace SteamCord.Application.Entities;

public class UserGuild
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ulong GuildId { get; set; }

    public User User { get; set; } = null!;
    public GuildConfig GuildConfig { get; set; } = null!;
}