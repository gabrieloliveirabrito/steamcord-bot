namespace SteamCord.Application.Entities;

public class UserGuild
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ulong GuildId { get; set; }
}