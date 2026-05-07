using Microsoft.Extensions.DependencyInjection;
using SteamCord.Application.Configuration;

namespace SteamCord.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection, string rootPath)
    {
        var envPath = Path.GetFullPath(Path.Combine(rootPath, "../.env"));
        collection.AddDotEnv<DiscordSettings>(envPath);
        collection.AddDotEnv<SteamSettings>(envPath);
        collection.AddDotEnv<AppSettings>(envPath);

        collection.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly);
        });

        return collection;
    }
}