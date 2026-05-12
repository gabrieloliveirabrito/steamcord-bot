using SteamCord.Application.SteamApis.Models;
using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Application.Interfaces.Services;

public interface ISteamApisService
{
    Task<SteamUser?> GetSteamUserAsync(string steamId, CancellationToken ct = default);
    Task<SteamAppDetails?> GetSteamAppDetailsAsync(string appId, CancellationToken ct = default);
}