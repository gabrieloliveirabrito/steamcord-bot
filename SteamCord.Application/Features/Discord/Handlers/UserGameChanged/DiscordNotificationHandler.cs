using MediatR;
using Microsoft.Extensions.Logging;
using SteamCord.Application.Features.Discord.Events;
using SteamCord.Application.Interfaces.Services;

namespace SteamCord.Application.Features.Discord.Handlers.UserGameChanged;

public class DiscordNotificationHandler(ILogger<DiscordNotificationHandler> logger, ISteamService steamService, IDiscordService discordService) 
: INotificationHandler<UserChangedGameEvent>
{
    public async Task Handle(UserChangedGameEvent notification, CancellationToken cancellationToken)
    {
        var fromApp = await steamService.GetAppsData([notification.FromAppId], cancellationToken);
        if (fromApp is null)
        {
            logger.LogError("Failed to fetch app {0} details", notification.FromAppId);
            return;
        }

        var fromAppDetail = fromApp[notification.FromAppId];
        if (!fromAppDetail.Success)
        {
            logger.LogError("Failed to get data of app {0}", notification.FromAppId);
            return;
        }

        var toApp = await steamService.GetAppsData([notification.ToAppId], cancellationToken);
        if (toApp is null)
        {
            logger.LogError("Failed to fetch app {0} details", notification.ToAppId);
            return;
        }

        var toAppDetail = toApp[notification.ToAppId];
        if (!toAppDetail.Success)
        {
            logger.LogError("Failed to get data of new app {0}", notification.ToAppId);
            return;
        }
        
        await discordService.SendGameChanged(notification.User, notification.GuildConfig, fromAppDetail.Data, toAppDetail.Data, notification.OcurredAt, cancellationToken);
    }
}