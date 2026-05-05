namespace SteamCord.Infrastructure.Persistence.Entities;

public class UserToken
{
    public int Id { get; set; }
    public ulong DiscordUserId { get; set; }
    public ulong GuildId { get; set; }
    public string Token { get; set; } = Guid.NewGuid().ToString();
    public bool Used { get; set; }
    public DateTime ExpiresAt { get; set; }
}