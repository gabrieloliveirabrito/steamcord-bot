using DotEnv.Core;

namespace SteamCord.Application.Configuration;

public class SteamSettings
{
    [EnvKey("STEAM_API_KEY")]
    public string ApiKey { get; set; } = null!;

    [EnvKey("STEAMAPIS_API_KEY")]
    public string SteamApisApiKey { get; set; } = null!;

    [EnvKey("STEAM_AUTH_CALLBACK")]
    public string AuthCallback { get; set; } = null!;

    [EnvKey("STEAM_AUTH_URL")]
    public string AuthUrl { get; set; } = null!;
}