using MediatR;

namespace SteamCord.Application.Features.Discord.Events;

public record UserChangedGameEvent(
    long UserID,
    ulong DiscordID,
    string SteamID,
    string OldGameID,
    string OldGameName,
    string NewGameID,
    string NewGameName,
    DateTime OcurredAt
) : INotification;