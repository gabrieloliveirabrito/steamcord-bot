using AspNet.Security.OpenId.Steam;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SteamCord.Application.Configuration;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces;
using SteamCord.Application.Interfaces.Repositories;
using SteamCord.Infrastructure.Persistence;
using SteamCord.Infrastructure.Persistence.Repositories;
using SteamCord.Infrastructure.Services;

namespace SteamCord.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection)
    {
        using var services = collection.BuildServiceProvider();
        var appSettings = services.GetRequiredService<AppSettings>();
        var steamSettings = services.GetRequiredService<SteamSettings>();

        collection.AddDbContext<AppDbContext>(options =>
        {            
            options.UseNpgsql(appSettings.AppDbConnectString);
        });

        collection.AddSingleton<IDiscordService, DiscordService>();
        collection.AddHttpClient<ISteamService, SteamService>()
        .AddPolicyHandler(GetRetry())
        .AddPolicyHandler(GetTimeout());

        collection.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = SteamAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie("Cookies")
        .AddSteam(options =>
        {
            options.ApplicationKey = steamSettings.ApiKey;
            options.CallbackPath = steamSettings.AuthCallback;
        });

        collection.AddScoped<IUserTokenRepository, UserTokenRepository>();
        collection.AddScoped<IGuildConfigRepository, GuildConfigRepository>();
        collection.AddScoped<IUserTokenRepository, UserTokenRepository>();

        collection.AddControllersWithViews();

        return collection;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetry()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));

    private static IAsyncPolicy<HttpResponseMessage> GetTimeout()
        => Policy.TimeoutAsync<HttpResponseMessage>(5);
}