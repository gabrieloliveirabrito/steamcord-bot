using SteamCord.Application.Entities;

namespace SteamCord.Application.Interfaces.Repositories;

public interface IUserGuildRepository
{
    //Task<List<UserGuild>> GetUserGuildsAsync(int userId, CancellationToken ct = default);

    Task AddAsync(UserGuild userGuild, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}