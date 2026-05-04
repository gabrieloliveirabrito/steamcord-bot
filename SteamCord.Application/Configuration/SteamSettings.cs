using DotEnv.Core;

namespace SteamCord.Application.Configuration;

public class SteamSettings
{
    [EnvKey("STEAM_API_KEY")]
    public string ApiKey { get; set; } = null!;
}