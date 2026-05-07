using Microsoft.EntityFrameworkCore;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Repositories;

namespace SteamCord.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        var existsUser = await appDbContext.Users.FirstOrDefaultAsync(x => x.DiscordId == user.DiscordId, ct);
        if (existsUser is not null)
        {
            existsUser.DiscordId = user.DiscordId;
            existsUser.SteamId = user.SteamId;
            existsUser.LastGameId = null;
        }
        else
        {
            await appDbContext.Users.AddAsync(user);
        }
    }

    public Task<User?> GetUserByDiscordIdAsync(ulong discordId, CancellationToken ct = default)
    {
        return appDbContext.Users.FirstOrDefaultAsync(x => x.DiscordId == discordId, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default) => appDbContext.SaveChangesAsync(ct);
}