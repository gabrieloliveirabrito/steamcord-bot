using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces;
using SteamCord.Infrastructure.Persistence;
using SteamCord.Infrastructure.Services;

namespace SteamCord.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection)
    {
        collection.AddDbContext<AppDbContext>((services, options) =>
        {
            var settings = services.GetRequiredService<AppSettings>();
            
            options.UseNpgsql(settings.AppDbConnectString);
        });

        collection.AddSingleton<IDiscordService, DiscordService>();
        collection.AddHttpClient<ISteamService, SteamService>()
        .AddPolicyHandler(GetRetry())
        .AddPolicyHandler(GetTimeout());

        return collection;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetry()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));

    private static IAsyncPolicy<HttpResponseMessage> GetTimeout()
        => Policy.TimeoutAsync<HttpResponseMessage>(5);
}