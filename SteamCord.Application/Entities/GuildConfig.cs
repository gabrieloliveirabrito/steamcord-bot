namespace SteamCord.Application.Entities;

public class GuildConfig
{
    public int Id { get; set; }
    public ulong GuildId { get; set; }
    public ulong ChannelId { get; set; }

    public ICollection<UserGuild> UserGuilds { get; set; } = [];
}