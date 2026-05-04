using DotEnv.Core;

namespace SteamCord.Application.Configuration;

public class DiscordSettings
{
    [EnvKey("DISCORD_TOKEN")]
    public string DiscordToken { get; set; } = null!;

    [EnvKey("DISCORD_PUBLIC_KEY")]
    public string DiscordPublicKey { get; set; } = null!;
}