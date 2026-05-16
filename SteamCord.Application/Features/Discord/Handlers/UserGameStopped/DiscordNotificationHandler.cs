using MediatR;
using Microsoft.Extensions.Logging;
using SteamCord.Application.Features.Discord.Events;
using SteamCord.Application.Interfaces.Services;

namespace SteamCord.Application.Features.Discord.Handlers.UserGameStopped;

public class DiscordNotificationHandler(ILogger<DiscordNotificationHandler> logger, ISteamService steamService, IDiscordService discordService) 
: INotificationHandler<UserStoppedGameEvent>
{
    public async Task Handle(UserStoppedGameEvent notification, CancellationToken cancellationToken)
    {
        var appId = notification.AppId;
        var apps = await steamService.GetAppsData([appId], cancellationToken);

        if (apps is null)
        {
            logger.LogError("Failed to fetch app {0} details", notification.User.LastGameId);
            return;
        }

        var appDetail = apps[appId];
        if (!appDetail.Success)
        {
            logger.LogError("Failed to get data of app {0}", appId);
            return;
        }
        
        await discordService.SendGameStopped(notification.User, notification.GuildConfig, appDetail.Data, notification.OcurredAt, cancellationToken);
    }
}