using MediatR;
using SteamCord.Application.Entities;

namespace SteamCord.Application.Features.Discord.Events;

public record UserChangedGameEvent(
    string FromAppId,
    string ToAppId,
    User User,
    GuildConfig GuildConfig,
    DateTime OcurredAt
) : INotification;