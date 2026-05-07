using MediatR;
using Microsoft.Extensions.Logging;
using SteamCord.Application.Common;
using SteamCord.Application.Entities;
using SteamCord.Application.Features.Steam.Commands;
using SteamCord.Application.Interfaces.Repositories;

namespace SteamCord.Application.Features.Steam.Handlers;

public class SetSteamChannelHandler(ILogger<SetSteamChannelHandler> logger, IGuildConfigRepository guildConfigRepository) : IRequestHandler<SetSteamChannelCommand, Result>
{
    public async Task<Result> Handle(SetSteamChannelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var config = new GuildConfig { GuildId = request.GuildId, ChannelId = request.ChannelId };
            await guildConfigRepository.AddAsync(config, cancellationToken);
            await guildConfigRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on GuildConfig handler!");
            return Result.Fail(ex.Message);
        }
    }
}