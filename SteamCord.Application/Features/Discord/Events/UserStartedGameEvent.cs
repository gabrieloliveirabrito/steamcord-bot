using MediatR;

namespace SteamCord.Application.Features.Discord.Events;

public record UserStartedGameEvent(
    long UserID,
    ulong DiscordID,
    string SteamID,
    string GameID,
    string GameName
) : INotification;