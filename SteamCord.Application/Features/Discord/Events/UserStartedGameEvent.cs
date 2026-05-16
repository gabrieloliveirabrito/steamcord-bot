using MediatR;
using SteamCord.Application.Entities;
using SteamCord.Application.SteamApis.Models;

namespace SteamCord.Application.Features.Discord.Events;

public record UserStartedGameEvent(
    string AppId,
    User User,
    GuildConfig GuildConfig,
    DateTime OcurredAt
) : INotification;