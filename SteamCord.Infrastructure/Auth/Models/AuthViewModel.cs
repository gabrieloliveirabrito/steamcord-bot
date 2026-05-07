namespace SteamCord.Infrastructure.Auth.Models;

public class AuthViewModel
{
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;

    public string? SteamName { get; set; }
    public string? SteamAvatarUrl { get; set; }

    public bool Success { get; set; }
}