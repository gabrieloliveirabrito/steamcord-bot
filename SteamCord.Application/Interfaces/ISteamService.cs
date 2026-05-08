using SteamCord.Application.SteamApis.Models;
using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Application.Interfaces;

public interface ISteamService
{
    Task<string?> GetGameBanner(int appId);
    Task<SteamUser?> GetSteamUserAsync(string steamId, CancellationToken ct = default);
}