using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Repositories;
using SteamCord.Infrastructure.Persistence;
using SteamCord.Infrastructure.Services;

namespace SteamCord.Bot.Workers;

public class SteamPresenceWorker(
    ILogger<SteamPresenceWorker> logger,
    IMediator mediator,
    IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(60));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Presence Worker started!");

        do
        {
            try
            {
                logger.LogDebug("Presence Worker Tick");

                await ProcessAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Critical error on Presence Worker!");
            }
        } while (await _timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        // var users = await userRepository.GetUsersAsync(cancellationToken);

        // foreach (var user in users)
        // {
        //     var userGuilds = await userGuildRepository.GetUserGuildsAsync(user.Id);

        //     foreach (var userGuild in userGuilds)
        //     {
        //         var guildConfig = await guildConfigRepository.GetGuildConfigAsync(userGuild.GuildId);

        //         if (guildConfig is not null)
        //         {

        //         }
        //     }
        // }

        using var scope = serviceScopeFactory.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var steam = scope.ServiceProvider.GetRequiredService<SteamService>();

        var users = await appDbContext.Users.Include(x => x.UserGuilds).ThenInclude(x => x.GuildConfig).ToListAsync(cancellationToken);
        if (users is null)
        {
            logger.LogWarning("Failed to fetch the users on Presence Worker");
            return;
        }
        logger.LogInformation("Processing {0} users", users.Count);

        await ProcessUsers(users, steam, cancellationToken);
    }

    async Task ProcessUsers(List<User> users, SteamService steamService, CancellationToken cancellationToken)
    {
        var batchIndex = 0;
        foreach (var batch in users.Chunk(100))
        {
            batchIndex++;

            var steamIds = batch.Select(x => x.SteamId).ToArray();
            var players = await steamService.GetPlayerSummaries(steamIds, cancellationToken);

            if (players is null)
            {
                logger.LogWarning("Failed to process batch {0} of steam users!", batchIndex);
                continue;
            }

            var map = players.ToDictionary(x => x.Id);
            foreach (var user in batch)
            {
                if (!map.TryGetValue(user.SteamId, out var steamPlayer))
                {
                    continue;
                }

                //TODO: Discord Presence
            }
        }
    }
}