using SteamCord.Application.Entities;

namespace SteamCord.Application.Interfaces.Repositories;

public interface IUserGuildRepository
{
    Task AddAsync(UserGuild userGuild, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}