using Microsoft.EntityFrameworkCore;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Repositories;

namespace SteamCord.Infrastructure.Persistence.Repositories;

public class UserGuildRepository(AppDbContext appDbContext) : IUserGuildRepository
{
    public async Task AddAsync(UserGuild userGuild, CancellationToken ct = default)
    {
        var exists = await appDbContext.UserGuilds.FirstOrDefaultAsync(x => x.UserId == userGuild.UserId && x.GuildId == userGuild.GuildId, ct);

        if (exists is null)
        {
            await appDbContext.UserGuilds.AddAsync(userGuild, ct);
        }
    }

    public Task SaveChangesAsync(CancellationToken ct = default) => appDbContext.SaveChangesAsync(ct);
}