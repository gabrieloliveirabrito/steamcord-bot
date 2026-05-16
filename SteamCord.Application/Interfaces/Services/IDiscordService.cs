using SteamCord.Application.Entities;
using SteamCord.Application.Steam.Models.Apps;
using SteamCord.Application.SteamApis.Models;

namespace SteamCord.Application.Interfaces.Services;

public interface IDiscordService
{
    Task SendHelloWorld(ulong channelId);

    Task SendGameStarted(User user, GuildConfig guildConfig, SteamGameData steamGameData, DateTime ocurredAt, CancellationToken cancellationToken);
    Task SendGameStopped(User user, GuildConfig guildConfig, SteamGameData steamGameData, DateTime ocurredAt, CancellationToken cancellationToken);
    Task SendGameChanged(User user, GuildConfig guildConfig, SteamGameData fromGameData, SteamGameData toGameData, DateTime ocurredAt, CancellationToken cancellationToken);
}