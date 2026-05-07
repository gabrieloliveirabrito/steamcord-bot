using System.Security.Claims;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SteamCord.Application.Configuration;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Repositories;
using SteamCord.Infrastructure.Auth.Models;
using SteamCord.Infrastructure.Persistence;

namespace SteamCord.Infrastructure.Auth.Controllers;

[ApiController]
[Route("auth/steam")]
public class SteamAuthController(
    //ILogger<SteamAuthController> logger,
    IUserRepository userRepository,
    IUserTokenRepository userTokenRepository,
    IUserGuildRepository userGuildRepository,
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
            return BadRequest("Steam authentication failed");

        var steamId = auth.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (steamId is null)
            return BadRequest("SteamId not found");

        var token = auth.Properties?.Items["token"];
        if (string.IsNullOrEmpty(token))
            return BadRequest("Token not found");

        var userToken = await userTokenRepository.FindByTokenAsync(token, ct);
        if (userToken is null)
            return BadRequest("Invalid token");

        if (userToken.Used || userToken.ExpiresAt < DateTime.UtcNow)
            return BadRequest("Token expired or already used");

        userToken.Used = true;
        await userTokenRepository.SaveChangesAsync(ct);

        var user = await userRepository.GetUserByDiscordIdAsync(userToken.DiscordUserId, ct);
        if (user is null)
        {
            user = new User
            {
                DiscordId = userToken.DiscordUserId,
                SteamId = steamId
            };

            await userRepository.AddAsync(user);
        }
        else
        {
            user.SteamId = steamId;
        }

        await userRepository.SaveChangesAsync(ct);

        var userGuild = new UserGuild { GuildId = userToken.GuildId, UserId = user.Id };
        await userGuildRepository.AddAsync(userGuild, ct);

        await userGuildRepository.SaveChangesAsync(ct);

        return Ok("Steam account linked successfully");
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        var model = new AuthViewModel
        {
            Success = true,
            SteamName = "Gabriel",
            SteamAvatarUrl = "https://avatars.cloudflare.steamstatic.com/5e7947e664e7a507e8a52dc563b8647519a05230_full.jpg"
        };

        return View("Success", model);
    }

    [HttpGet("error")]
    public IActionResult Error()
    {
        return View("Error", new AuthViewModel
        {
            Success = false,
            Message = "An unexpected authentication error occurred."
        });
    }

    [HttpGet("expired")]
    public IActionResult Expired()
    {
        return View("Expired", new AuthViewModel
        {
            Success = false,
            Message = "Your login token expired."
        });
    }
}