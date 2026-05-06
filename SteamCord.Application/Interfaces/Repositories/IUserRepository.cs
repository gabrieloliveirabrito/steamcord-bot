using SteamCord.Application.Entities;

namespace SteamCord.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByDiscordIdAsync(ulong discordId, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}