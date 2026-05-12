using Microsoft.Extensions.Logging;
using SteamCord.Application.Interfaces;
using SteamCord.Application.SteamApis.Models.Users;

namespace SteamCord.Infrastructure.Services;

public class SteamService(ILogger<SteamService> logger, HttpClient http) : ISteamService
{
    public Task<SteamUser?> GetPlayerSummaries(string steamId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}