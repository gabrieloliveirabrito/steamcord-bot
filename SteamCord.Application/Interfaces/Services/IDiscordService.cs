using SteamCord.Application.Entities;
using SteamCord.Application.SteamApis.Models;

namespace SteamCord.Application.Interfaces.Services;

public interface IDiscordService
{
    Task SendHelloWorld(ulong channelId);

    Task SendGameStarted(User user, GuildConfig guildConfig, SteamAppDetails appDetails, DateTime ocurredAt, CancellationToken cancellationToken);
}