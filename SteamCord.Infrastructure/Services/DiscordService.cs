using Microsoft.Extensions.Logging;
using NetCord;
using NetCord.Rest;
using SteamCord.Application.Entities;
using SteamCord.Application.Interfaces.Services;
using SteamCord.Application.SteamApis.Models;
using SCUser = SteamCord.Application.Entities.User;

namespace SteamCord.Infrastructure.Services;

public class DiscordService(ILogger<DiscordService> logger, RestClient restClient) : IDiscordService
{
    async Task<(NetCord.User DiscordUser, RestGuild DiscordGuild, Channel DiscordChannel)?> FetchDiscordData(SCUser user, GuildConfig guildConfig, CancellationToken cancellationToken)
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

        var discordChannel = await restClient.GetChannelAsync(guildConfig.ChannelId, cancellationToken: cancellationToken);
        if (discordChannel is null)
        {
            logger.LogError("Failed to find discord channel {0}", guildConfig.ChannelId);
            return null;
        }

        return (discordUser, discordGuild, discordChannel);
    }

    public async Task SendGameStarted(SCUser user, GuildConfig guildConfig, SteamAppDetails appDetails, DateTime ocurredAt, CancellationToken cancellationToken)
    {
        var info = await FetchDiscordData(user, guildConfig, cancellationToken);
        if (info is null)
        {
            return;
        }
        var (discordUser, discordGuild, discordChannel) = info.Value;

        var publisher = appDetails.Publishers.FirstOrDefault();
        var releaseDate = (appDetails.ReleaseDate?.ComingSoon is true ? "Comming Soon" : appDetails.ReleaseDate?.Date) ?? "Unknown";

        var message = new MessageProperties { };
        message.AddEmbeds(new EmbedProperties
        {
            Title = $"O jogador {discordUser.Username} iniciou o jogo **{appDetails.Name}**",
            Description = appDetails.ShortDescription,
            Color = new Color(0x00FF00),
            Image = appDetails.HeaderImage,
            Url = $"https://store.steampowered.com/app/{appDetails.Id}",
            Timestamp = DateTimeOffset.UtcNow,
            Author = new EmbedAuthorProperties
            {
                Name = publisher,
                Url = appDetails.Website
            },
            Fields = [
                new EmbedFieldProperties { Inline = true, Name = "Genres", Value = string.Join(", ", appDetails.Genres.Select(x => x.Description)) },
                new EmbedFieldProperties { Inline = true, Name = "Free", Value = appDetails.IsFree ? "Yes" : "No" },
                new EmbedFieldProperties { Inline = true, Name = "Success", Value = appDetails.IsSuccess ? "Yes" : "No" },
                new EmbedFieldProperties { Inline = true, Name = "Recommendations", Value = (appDetails.Recommendations?.Total ?? 0).ToString() },
                new EmbedFieldProperties { Inline = true, Name = "Price", Value = appDetails.PriceOverview?.FinalFormatted ?? "Unknown" },
                new EmbedFieldProperties { Inline = true, Name = "Released At", Value = releaseDate }
            ],
            Footer = new EmbedFooterProperties
            {
                Text = $"Enviado em {discordGuild.Name}",
                IconUrl = discordGuild.GetIconUrl()?.ToString()
            }
        });

        await restClient.SendMessageAsync(discordChannel.Id, message, cancellationToken: cancellationToken);
    }

    public Task SendHelloWorld(ulong channelId) => restClient.SendMessageAsync(channelId, "Hello World");
}