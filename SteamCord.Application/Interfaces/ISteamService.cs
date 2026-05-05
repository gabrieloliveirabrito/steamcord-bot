namespace SteamCord.Application.Interfaces;

public interface ISteamService
{
    Task<string?> GetGameBanner(int appId);
    Task<string?> GetGameHeader(int appId);
}