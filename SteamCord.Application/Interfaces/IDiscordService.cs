namespace SteamCord.Application.Interfaces;

public interface IDiscordService
{
    Task SendHelloWorld(ulong channelId);
}