using SteamCord.Application.Entities;

namespace SteamCord.Application.Interfaces.Repositories;

public interface IUserTokenRepository
{
    Task<UserToken?> GetTokenAsync(ulong discordUserId, ulong guildId, CancellationToken ct = default);
    Task AddAsync(UserToken userToken, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}