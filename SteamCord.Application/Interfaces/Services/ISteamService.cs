using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Application.Interfaces.Services;

public interface ISteamService
{
    Task<SteamUser?> GetPlayerSummaries(string steamId, CancellationToken ct = default);
}