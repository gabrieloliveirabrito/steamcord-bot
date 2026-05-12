namespace SteamCord.Application.Interfaces.Services;

public interface IDiscordService
{
    Task SendHelloWorld(ulong channelId);
}