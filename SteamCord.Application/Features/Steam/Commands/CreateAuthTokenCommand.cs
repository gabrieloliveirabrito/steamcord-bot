using MediatR;
using SteamCord.Application.Common;
using SteamCord.Application.Features.Steam.Responses;

namespace SteamCord.Application.Features.Steam.Commands;

public record CreateAuthTokenCommand(ulong DiscordUserId, ulong GuildId) : IRequest<CreateAuthTokenResponse>;