using DotEnv.Core;
using Microsoft.Extensions.DependencyInjection;
using SteamCord.Application.Configuration;

namespace SteamCord.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection, string rootPath)
    {
        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.Equals("true", StringComparison.InvariantCultureIgnoreCase) is true)
        {
            new EnvLoader().AvoidModifyEnvironment().Load();
            collection.AddDotEnv<DiscordSettings>();
            collection.AddDotEnv<SteamSettings>();
            collection.AddDotEnv<AppSettings>();
        }
        else
        {
            var envPath = Path.GetFullPath(Path.Combine(rootPath, "../.env"));

            collection.AddDotEnv<DiscordSettings>(envPath);
            collection.AddDotEnv<SteamSettings>(envPath);
            collection.AddDotEnv<AppSettings>(envPath);
        }

        collection.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly);
        });

        return collection;
    }
}