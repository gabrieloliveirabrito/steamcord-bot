namespace SteamCord.Infrastructure.Persistence.Entities;

public class User
{
    public int Id { get; set; }
    public ulong DiscordId { get; set; }
    public string SteamId { get; set; } = null!;
    public int? LastGameId { get; set; }
}