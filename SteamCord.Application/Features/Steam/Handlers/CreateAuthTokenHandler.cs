using MediatR;
using SteamCord.Application.Entities;
using SteamCord.Application.Features.Steam.Commands;
using SteamCord.Application.Features.Steam.Responses;
using SteamCord.Application.Interfaces.Repositories;

namespace SteamCord.Application.Features.Steam.Handlers;

public class CreateAuthTokenHandler(IUserTokenRepository userTokenRepository, IGuildConfigRepository guildConfigRepository) 
: IRequestHandler<CreateAuthTokenCommand, CreateAuthTokenResponse>
{
    public async Task<CreateAuthTokenResponse> Handle(CreateAuthTokenCommand request, CancellationToken cancellationToken)
    {
        var guildConfig = await guildConfigRepository.GetGuildConfigAsync(request.GuildId, cancellationToken);
        if (guildConfig is null)
        {
            return new CreateAuthTokenResponse(false)
            {
                Error = "This guild hasn't configured!"
            };
        }

        var userToken = await userTokenRepository.GetTokenAsync(request.DiscordUserId, request.GuildId, cancellationToken);

        if (userToken is null)
        {
            userToken = new UserToken
            {
                DiscordUserId = request.DiscordUserId,
                GuildId = request.GuildId,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(10), //TODO
                Token = Guid.NewGuid().ToString("N"),
                Used = false
            };

            await userTokenRepository.AddAsync(userToken, cancellationToken);
        }
        else if (!userToken.Used && userToken.ExpiresAt > DateTimeOffset.Now)
        {
            return new CreateAuthTokenResponse(true)
            {
                Token = userToken.Token
            };
        }
        else
        {
            userToken.Used = false;
            userToken.ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(10); //TODO
            userToken.Token = Guid.NewGuid().ToString("N");
        }

        await userTokenRepository.SaveChangesAsync(cancellationToken);
        return new CreateAuthTokenResponse(true)
        {
            Token = userToken.Token
        };
    }
}