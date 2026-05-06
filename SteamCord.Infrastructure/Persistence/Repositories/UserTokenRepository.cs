using Microsoft.EntityFrameworkCore;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Repositories;

namespace SteamCord.Infrastructure.Persistence.Repositories;

public class UserTokenRepository(AppDbContext appDbContext) : IUserTokenRepository
{
    public async Task AddAsync(UserToken userToken, CancellationToken ct = default)
    {
        await appDbContext.UserTokens.AddAsync(userToken, ct);
    }

    public async Task<UserToken?> GetTokenAsync(ulong discordUserId, ulong guildId, CancellationToken ct = default)
    {
        return await appDbContext.UserTokens.FirstOrDefaultAsync(x => x.DiscordUserId == discordUserId && x.GuildId == guildId, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return appDbContext.SaveChangesAsync();
    }
}