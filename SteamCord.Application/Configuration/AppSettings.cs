using DotEnv.Core;

namespace SteamCord.Application.Configuration;

public class AppSettings
{
    [EnvKey("APP_DB_CONN_STRING")]
    public string AppDbConnectString { get; set; } = null!;

    [EnvKey("APP_REDIS_CONN_STRING")]
    public string AppRedisConnectString { get; set; } = null!;
}