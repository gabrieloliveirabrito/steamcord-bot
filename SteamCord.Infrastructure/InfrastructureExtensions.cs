using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using StackExchange.Redis;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces.Services;
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
        collection.AddHttpClient<ISteamApisService, SteamApisService>((services, options) =>
        {
            var settings = services.GetRequiredService<SteamSettings>();

            options.BaseAddress = new Uri("https://api.steamapis.com/v2/steam/");
            options.DefaultRequestHeaders.Add("x-api-key", settings.SteamApisApiKey);
        })
        .AddPolicyHandler(GetRetry())
        .AddPolicyHandler(GetTimeout());

        collection.AddHttpClient<ISteamService, SteamService>((services, options) =>
        {
            var settings = services.GetRequiredService<SteamSettings>();

            options.BaseAddress = new Uri("https://api.steampowered.com/");
        }).AddPolicyHandler(GetRetry()).AddPolicyHandler(GetTimeout());

        collection.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = SteamAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        })
        .AddSteam(options =>
        {
            options.ApplicationKey = steamSettings.ApiKey;
            //options.CallbackPath = steamSettings.AuthCallback;
        });

        collection.AddScoped<IGuildConfigRepository, GuildConfigRepository>();
        collection.AddScoped<IUserTokenRepository, UserTokenRepository>();
        collection.AddScoped<IUserGuildRepository, UserGuildRepository>();
        collection.AddScoped<IUserRepository, UserRepository>();

        collection.AddControllersWithViews()
        .AddApplicationPart(typeof(InfrastructureExtensions).Assembly)
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
        })
        .AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Add(
                "/Auth/Views/{0}.cshtml");

            options.ViewLocationFormats.Add(
                "/Auth/Views/Shared/{0}.cshtml");
        });

        collection.AddSingleton<IConnectionMultiplexer>(services =>
        {
            var appSettings = services.GetRequiredService<AppSettings>();

            return ConnectionMultiplexer.Connect(appSettings.AppRedisConnectString
#if DEBUG
             , Console.Out
#endif
            );
        });
        collection.AddSingleton<IRedisService, RedisService>();

        return collection;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetry()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));

    private static IAsyncPolicy<HttpResponseMessage> GetTimeout()
        => Policy.TimeoutAsync<HttpResponseMessage>(15);
}