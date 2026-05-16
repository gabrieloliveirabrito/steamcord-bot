using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SteamCord.Application.Entities;
using SteamCord.Application.Features.Discord.Events;
using SteamCord.Application.Interfaces.Repositories;
using SteamCord.Application.Interfaces.Services;
using SteamCord.Application.Steam.Models.Players;
using SteamCord.Infrastructure.Persistence;
using SteamCord.Infrastructure.Persistence.Repositories;
using SteamCord.Infrastructure.Services;

namespace SteamCord.Bot.Workers;

public class SteamPresenceWorker(
    ILogger<SteamPresenceWorker> logger,
    IMediator mediator,
    ISteamService steamService,
    IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(20));

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
        using var scope = serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var users = await userRepository.GetTrackedUsersAsync(cancellationToken);
        if (users is null)
        {
            logger.LogWarning("Failed to fetch the users on Presence Worker");
            return;
        }
        logger.LogInformation("Processing {0} users", users.Count);

        await ProcessUsersAsync(users, userRepository, cancellationToken);
    }

    async Task ProcessUsersAsync(List<User> users, IUserRepository userRepository, CancellationToken cancellationToken)
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
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                if (!map.TryGetValue(user.SteamId, out var steamPlayer))
                {
                    continue;
                }

                await ProcessUserAsync(user, steamPlayer, userRepository, cancellationToken);
            }
        }
    }

    async Task ProcessUserAsync(User user, SteamPlayer player, IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var lastGameId = user.LastGameId;
        var currentGameId = player.GameID;

        logger.LogInformation($"{lastGameId ?? "UNKWN"} | {currentGameId ?? "UNKNW"}");
        if (string.Equals(lastGameId, currentGameId))
            return;

        await userRepository.UpdatePresenceAsync(user.Id, currentGameId, player.GameExtraInfo, DateTime.UtcNow, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        foreach (var guild in user.UserGuilds)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            if (string.IsNullOrEmpty(lastGameId) && !string.IsNullOrEmpty(currentGameId))
            {
                logger.LogInformation($"User {user.SteamId} started game {player.GameID}");
                var notification = new UserStartedGameEvent(currentGameId, user, guild.GuildConfig, DateTime.UtcNow);

                await mediator.Publish(notification, cancellationToken);
            }
            else if (string.IsNullOrEmpty(currentGameId) && !string.IsNullOrEmpty(lastGameId))
            {
                logger.LogInformation($"User {user.SteamId} stopped game {player.GameID}");

                var notification = new UserStoppedGameEvent(lastGameId, user, guild.GuildConfig, DateTime.UtcNow);
                await mediator.Publish(notification, cancellationToken);
            }
            else if(!string.IsNullOrEmpty(lastGameId) && !string.IsNullOrEmpty(currentGameId))
            {
                logger.LogInformation($"User {user.SteamId} changed game {user.LastGameId} to {player.GameID}");

                var notification = new UserChangedGameEvent(lastGameId, currentGameId, user, guild.GuildConfig, DateTime.UtcNow);
                await mediator.Publish(notification, cancellationToken);
            }
        }
    }
}