using MediatR;
using SteamCord.Application.Common;

namespace SteamCord.Application.Features.Steam.Commands;

public record SetSteamChannelCommand(ulong GuildId, ulong ChannelId) : IRequest<Result>;