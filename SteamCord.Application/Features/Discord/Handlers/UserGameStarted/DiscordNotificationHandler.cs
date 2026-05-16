using MediatR;
using Microsoft.Extensions.Logging;
using SteamCord.Application.Features.Discord.Events;
using SteamCord.Application.Interfaces.Services;

namespace SteamCord.Application.Features.Discord.Handlers.UserGameStarted;

public class DIscordNotificationHandler(ILogger<DIscordNotificationHandler> logger, ISteamApisService steamApisService, IDiscordService discordService) 
: INotificationHandler<UserStartedGameEvent>
{
    public async Task Handle(UserStartedGameEvent notification, CancellationToken cancellationToken)
    {
        var appDetails = await steamApisService.GetSteamAppDetailsAsync(notification.User.LastGameId!, cancellationToken);
        if (appDetails is null)
        {
            logger.LogError("Failed to fetch app {0} details", notification.User.LastGameId);
            return;
        }
        await discordService.SendGameStarted(notification.User, notification.GuildConfig, appDetails, notification.OcurredAt, cancellationToken);
    }
}