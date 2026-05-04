using Microsoft.Extensions.DependencyInjection;
using SteamCord.Application.Interfaces;
using SteamCord.Infrastructure.Services;

namespace SteamCord.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection collection)
    {
        collection.AddSingleton<IDiscordService, DiscordService>();
        collection.AddSingleton<ISteamService, SteamService>();
    }
}