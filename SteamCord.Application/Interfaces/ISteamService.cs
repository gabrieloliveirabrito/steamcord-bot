using SteamCord.Application.SteamApis.Models;
using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Application.Interfaces;

public interface ISteamService
{
    Task<SteamUser?> GetPlayerSummaries(string steamId, CancellationToken ct = default);
}