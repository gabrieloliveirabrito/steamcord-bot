using SteamCord.Application.Steam.Models.Players;

namespace SteamCord.Application.Interfaces.Services;

public interface ISteamService
{
    Task<IReadOnlyList<SteamPlayer>?> GetPlayerSummaries(IEnumerable<string> steamIds, CancellationToken ct = default);
}