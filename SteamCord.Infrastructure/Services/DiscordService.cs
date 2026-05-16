using Microsoft.Extensions.Logging;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Services;
using SteamCord.Application.Steam.Models.Apps;
using SteamCord.Application.SteamApis.Models;
using SCUser = SteamCord.Application.Entities.User;

namespace SteamCord.Infrastructure.Services;

public class DiscordService(ILogger<DiscordService> logger, RestClient restClient) : IDiscordService
{
    async Task<(NetCord.User DiscordUser, RestGuild DiscordGuild)?> FetchDiscordData(SCUser user, GuildConfig guildConfig, CancellationToken cancellationToken)
    {
        
        var discordUser = await restClient.GetUserAsync(user.DiscordId, cancellationToken: cancellationToken);
        if (discordUser is null)
        {
            logger.LogError("Failed to get discord user {0}", user.DiscordId);
            return null;
        }

        var discordGuild = await restClient.GetGuildAsync(guildConfig.GuildId, true, cancellationToken: cancellationToken);
        if (discordGuild is null)
        {
            logger.LogError("Failed to find discord guild {0}", guildConfig.GuildId);
            return null;
        }

        return (discordUser, discordGuild);
    }

    EmbedProperties CreateGameEmbed(string description, Color embedColor, RestGuild discordGuild, SteamGameData steamGameData, DateTime ocurredAt)
    {

        var publisher = steamGameData.Developers.FirstOrDefault();
        var releaseDate = (steamGameData.ReleaseDate?.ComingSoon is true ? "Comming Soon" : steamGameData.ReleaseDate?.Date) ?? "Unknown";

        var platforms = new List<string>();
        if (steamGameData.Platforms.Windows)
            platforms.Add("Windows");
        if (steamGameData.Platforms.Mac)
            platforms.Add("Mac");
        if (steamGameData.Platforms.Linux)
            platforms.Add("Linux");

        return new EmbedProperties
        {
            Title = description,
            Description = steamGameData.ShortDescription,
            Color = embedColor,
            Image = steamGameData.HeaderImage,
            Url = $"https://store.steampowered.com/app/{steamGameData.SteamAppId}",
            Timestamp = ocurredAt,
            Author = new EmbedAuthorProperties
            {
                Name = publisher,
                Url = steamGameData.Website
            },
            Fields = [
                new EmbedFieldProperties { Inline = true, Name = "Platforms", Value = string.Join(", ", platforms) },
                new EmbedFieldProperties { Inline = true, Name = "Genres", Value = string.Join(", ", steamGameData.Genres.Select(x => x.Description)) },
                new EmbedFieldProperties { Inline = true, Name = "Categories", Value = string.Join(",", steamGameData.Categories.Select(x => x.Description)) },
                new EmbedFieldProperties { Inline = true, Name = "Free", Value = steamGameData.IsFree ? "Yes" : "No" },
                new EmbedFieldProperties { Inline = true, Name = "Recommendations", Value = (steamGameData.Recommendations?.Total ?? 0).ToString() },
                new EmbedFieldProperties { Inline = true, Name = "Released At", Value = releaseDate }
            ],
            Footer = new EmbedFooterProperties
            {
                Text = $"Enviado em {discordGuild.Name}",
                IconUrl = discordGuild.GetIconUrl()?.ToString()
            }
        };
    }

    public async Task SendGameStarted(SCUser user, GuildConfig guildConfig, SteamGameData steamGameData, DateTime ocurredAt, CancellationToken cancellationToken)
    {
        var info = await FetchDiscordData(user, guildConfig, cancellationToken);
        if (info is null)
        {
            return;
        }
        var (discordUser, discordGuild) = info.Value;

        var message = new MessageProperties { };
        message.AddEmbeds(CreateGameEmbed($"O jogador {discordUser.Username} iniciou o jogo **{steamGameData.Name}**", new Color(0x00FF00), discordGuild, steamGameData, ocurredAt));

        await restClient.SendMessageAsync(guildConfig.ChannelId, message, cancellationToken: cancellationToken);
    }

    public async Task SendGameStopped(SCUser user, GuildConfig guildConfig, SteamGameData steamGameData, DateTime ocurredAt, CancellationToken cancellationToken)
    {
        var info = await FetchDiscordData(user, guildConfig, cancellationToken);
        if (info is null)
        {
            return;
        }
        var (discordUser, discordGuild) = info.Value;

        var message = new MessageProperties { };
        message.AddEmbeds(CreateGameEmbed($"O jogador {discordUser.Username} encerrou o jogo **{steamGameData.Name}**", new Color(0xFF0000), discordGuild, steamGameData, ocurredAt));

        await restClient.SendMessageAsync(guildConfig.ChannelId, message, cancellationToken: cancellationToken);
    }

    public async Task SendGameChanged(SCUser user, GuildConfig guildConfig, SteamGameData fromGameData, SteamGameData toGameData, DateTime ocurredAt, CancellationToken cancellationToken)
    {
        var info = await FetchDiscordData(user, guildConfig, cancellationToken);
        if (info is null)
        {
            return;
        }
        var (discordUser, discordGuild) = info.Value;

        var message = new MessageProperties { };
        message.AddEmbeds(CreateGameEmbed($"O jogador {discordUser.Username} trocou o jogo de **{fromGameData.Name}** para **{toGameData.Name}**", new Color(0x0000FF), discordGuild, toGameData, ocurredAt));

        await restClient.SendMessageAsync(guildConfig.ChannelId, message, cancellationToken: cancellationToken);
    }

    public Task SendHelloWorld(ulong channelId) => restClient.SendMessageAsync(channelId, "Hello World");
}