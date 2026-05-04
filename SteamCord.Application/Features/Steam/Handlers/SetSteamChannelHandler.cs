using MediatR;
using SteamCord.Application.Common;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Application.Features.Steam.Handlers;

public class SetSteamChannelHandler : IRequestHandler<SetSteamChannelCommand, Result>
{
    public Task<Result> Handle(SetSteamChannelCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Ok());
    }
}