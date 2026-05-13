using SteamCord.Application.Entities;

namespace SteamCord.Application.Interfaces.Repositories;

public interface IGuildConfigRepository
{
    public Task<GuildConfig?> GetGuildConfigAsync(ulong guildId, CancellationToken ct = default);

    public Task AddAsync(GuildConfig guildConfig, CancellationToken ct = default);
    public Task SaveChangesAsync(CancellationToken ct = default);
}