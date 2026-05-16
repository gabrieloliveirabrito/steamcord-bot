using MediatR;
using SteamCord.Application.Entities;

namespace SteamCord.Application.Features.Discord.Events;

public record UserStoppedGameEvent(
    string AppId,
    User User,
    GuildConfig GuildConfig,
    DateTime OcurredAt
) : INotification;