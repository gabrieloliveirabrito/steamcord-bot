#nullable disable
using DotEnv.Core;

namespace SteamCord.Bot;

public class AppSettings
{
    [EnvKey("DISCORD_TOKEN")]
    public string DiscordToken { get; set; }

    [EnvKey("DISCORD_PUBLIC_KEY")]
    public string DiscordPublicKey { get; set; }
}