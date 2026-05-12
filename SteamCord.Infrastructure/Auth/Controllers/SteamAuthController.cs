using System.Security.Claims;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SteamCord.Application.Configuration;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Services;
using SteamCord.Application.Interfaces.Repositories;
using SteamCord.Infrastructure.Auth.Models;

namespace SteamCord.Infrastructure.Auth.Controllers;

[ApiController]
[Route("auth/steam")]
public class SteamAuthController(
    //ILogger<SteamAuthController> logger,
    IUserRepository userRepository,
    IUserTokenRepository userTokenRepository,
    IUserGuildRepository userGuildRepository,
    ISteamApisService steamApisService,
    SteamSettings steamSettings
    ) : Controller
{
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string token)
    {
        var props = new AuthenticationProperties
        {
            RedirectUri = steamSettings.AuthCallback
        };
        props.Items["token"] = token;

        return Challenge(props, SteamAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(CancellationToken ct = default)
    {
        var auth = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!auth.Succeeded)
            return await SendErrorPage("Steam authentication failed", ct: ct);

        var steamId = auth.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value?.Split('/')?.Last();

        if (steamId is null)
            return await SendErrorPage("SteamId not found", ct: ct);

        var token = auth.Properties?.Items["token"];
        if (string.IsNullOrEmpty(token))
            return await SendErrorPage("Token not found", ct: ct);

        var userToken = await userTokenRepository.FindByTokenAsync(token, ct);
        if (userToken is null)
            return await SendErrorPage("Invalid token", ct: ct);

        if (userToken.Used || userToken.ExpiresAt < DateTime.UtcNow)
            return await SendErrorPage("Token expired or already used", "Expired", ct);

        userToken.Used = true;
        await userTokenRepository.SaveChangesAsync(ct);

        var user = await CreateUser(userToken.DiscordUserId, steamId, ct);
        await UpdateUserGuild(user.Id, userToken.GuildId, ct);

        return await SendSuccessPage(steamId, ct);
    }

    async Task<User> CreateUser(ulong discordUserId, string steamId, CancellationToken ct = default)
    {
        var user = await userRepository.GetUserByDiscordIdAsync(discordUserId, ct);
        if (user is null)
        {
            user = new User
            {
                DiscordId = discordUserId,
                SteamId = steamId
            };

            await userRepository.AddAsync(user, ct);
        }
        else
        {
            user.SteamId = steamId;
        }

        await userRepository.SaveChangesAsync(ct);
        return user;
    }

    async Task UpdateUserGuild(int userId, ulong guildId, CancellationToken ct = default)
    {
        var userGuild = new UserGuild { GuildId = guildId, UserId = userId };
        await userGuildRepository.AddAsync(userGuild, ct);

        await userGuildRepository.SaveChangesAsync(ct);
    }

    async Task<IActionResult> SendSuccessPage(string steamId, CancellationToken ct = default)
    {
        var user = await steamApisService.GetSteamUserAsync(steamId, ct);

        var model = new AuthViewModel
        {
            Success = true,
            SteamName = user?.DisplayName,
            SteamAvatarUrl = user?.Avatar.Fetch()
        };

        return View("Success", model);
    }

    async Task<IActionResult> SendErrorPage(string message, string view = "Error", CancellationToken ct = default)
    {
        var model = new AuthViewModel
        {
            Success = false,
            Message = message
        };

        return View(view, model);
    }

    [HttpGet("test-success")]
    public Task<IActionResult> TestSuccess([FromQuery] string steamId, CancellationToken ct = default)
        => SendSuccessPage(steamId, ct);
}