using MediatR;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Bot.Features.Steam.Commands;

public partial class BaseCommand
{
    [SubSlashCommand("test-embed", "Test the start/exit game embed")]
    public async Task<InteractionMessageProperties> SendTestEmbed(
        [SlashCommandParameter(Name = "start", Description = "Send the start or stop text")] bool start = true
    )
    {
        var message = new InteractionMessageProperties();

        message.AddEmbeds(new EmbedProperties
        {
            Author = new EmbedAuthorProperties
            {
                Name = Context.Interaction.User.GlobalName,
                IconUrl = Context.Interaction.User.GetAvatarUrl(ImageFormat.Png)?.ToString()
            },
            Url = $"https://store.steampowered.com/app/3357650",
            Image = "https://cdn.cloudflare.steamstatic.com/steam/apps/3357650/library_hero.jpg",
            Title = start ? "Notificação de abertura" : "Notificação de encerramento",
            Timestamp = DateTimeOffset.UtcNow,
            Footer = new EmbedFooterProperties
            {
                Text = $"Enviado em {Context.Guild?.Name}",
                IconUrl = Context.Guild?.GetIconUrl()?.ToString()
            },
            Color = new Color(0x0000FF),
            Description = $"O membro {Context.Interaction.User.GlobalName} {(start ? "abriu" : "fechou")} o jogo **PRAGMATA**"
        });

        return message;
    }
}