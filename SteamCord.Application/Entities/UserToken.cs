namespace SteamCord.Application.Entities;

public class UserToken
{
    public int Id { get; set; }
    public ulong DiscordUserId { get; set; }
    public ulong GuildId { get; set; }
    public string Token { get; set; } = Guid.NewGuid().ToString();
    public bool Used { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}