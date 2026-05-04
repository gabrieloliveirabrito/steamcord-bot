using NetCord.Rest;
using SteamCord.Application.Interfaces;

namespace SteamCord.Infrastructure.Services;

public class DiscordService(RestClient restClient) : IDiscordService
{
    public Task SendHelloWorld(ulong channelId) => restClient.SendMessageAsync(channelId, "Hello World");
}