using Microsoft.EntityFrameworkCore;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Repositories;

namespace SteamCord.Infrastructure.Persistence.Repositories;

public class GuildConfigRepository(AppDbContext appDbContext) : IGuildConfigRepository
{
    public async Task AddAsync(GuildConfig guildConfig, CancellationToken ct = default)
    {
        var exists = await appDbContext.GuildConfigs.FirstOrDefaultAsync(x => x.GuildId == guildConfig.GuildId, ct);

        if (exists is not null)
        {
            exists.ChannelId = guildConfig.ChannelId;
        }
        else
        {
            await appDbContext.GuildConfigs.AddAsync(guildConfig);
        }
    }

    public Task<GuildConfig?> GetGuildConfigAsync(ulong guildId, CancellationToken ct = default)
    => appDbContext.GuildConfigs.FirstOrDefaultAsync(x => x.GuildId == guildId, ct);

    public Task SaveChangesAsync(CancellationToken ct = default) => appDbContext.SaveChangesAsync(ct);
}