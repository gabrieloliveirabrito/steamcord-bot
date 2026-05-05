namespace SteamCord.Infrastructure.Persistence.Entities;

public class GuildConfig
{
    public int Id { get; set; }
    public ulong GuildId { get; set; }
    public ulong ChannelId { get; set; }
}