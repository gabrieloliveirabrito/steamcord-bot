using MediatR;

namespace SteamCord.Application.Features.Discord.Events;

public record UserStoppedGameEvent(
    long UserID,
    ulong DiscordID,
    string SteamID,
    string LastGameID,
    string LastGameName,
    DateTime OcurredAt
) : INotification;