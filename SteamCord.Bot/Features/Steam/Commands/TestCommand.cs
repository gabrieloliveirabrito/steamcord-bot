using MediatR;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Bot.Features.Steam.Commands;

public partial class BaseCommand
{
    [SubSlashCommand("test-embed", "Test the start/exit game embed")]
    public async Task SendTestEmbed(
        [SlashCommandParameter(Name = "start", Description = "Send the start or stop text")] bool start = true        
    )
    {
        string appId = "3357650";

        var embed = new EmbedProperties
        {
            /*Author = new EmbedAuthorProperties
            {
                Name = Context.Interaction.User.GlobalName,
                IconUrl = Context.Interaction.User.GetAvatarUrl(ImageFormat.Png)?.ToString()
            },*/
            //Url = $"https://store.steampowered.com/app/3357650",
            //Image = "https://cdn.cloudflare.steamstatic.com/steam/apps/3357650/library_hero.jpg",
            Title = start ? "Notificação de abertura" : "Notificação de encerramento",
            Timestamp = DateTimeOffset.UtcNow,
            Footer = new EmbedFooterProperties
            {
                Text = $"Enviado em {Context.Guild?.Name}",
                IconUrl = Context.Guild?.GetIconUrl()?.ToString()
            },
            Color = new Color(0x0000FF),
            //Description = $"O membro {Context.Interaction.User.GlobalName} {(start ? "abriu" : "fechou")} o jogo **PRAGMATA**"
        };

        var appDetails = await steamService.GetSteamAppDetailsAsync(appId);
        if (appDetails is null)
        {
            embed.WithColor(new Color(0xFF0000))
                 .WithTitle("Erro na API steam")
                 .WithDescription($"Falha ao obter os dados do app {appId}");
        }
        else
        {
            var publisher = appDetails.Publishers.FirstOrDefault();
            var releaseDate = (appDetails.ReleaseDate?.ComingSoon is true ? "Comming Soon" : appDetails.ReleaseDate?.Date) ?? "Unknown";

            embed.WithUrl($"https://store.steampowered.com/app/{appId}")
                 .WithDescription($"O membro {Context.Interaction.User.GlobalName} {(start ? "abriu" : "fechou")} o jogo **{appDetails.Name}**")
                 .WithImage(appDetails.HeaderImage)
                 .WithAuthor(string.IsNullOrEmpty(publisher) ? null : new EmbedAuthorProperties { Name = publisher, Url = appDetails.Website })
                 .AddFields([
                    //..appDetails.Genres.Select(x => new EmbedFieldProperties { Name = "Genre", Value = x.Description }),
                    new EmbedFieldProperties { Inline = true, Name = "Genres", Value = string.Join(", ", appDetails.Genres.Select(x => x.Description)) },
                    new EmbedFieldProperties { Name = "Free", Value = appDetails.IsFree ? "Yes" : "No" },
                    new EmbedFieldProperties { Name = "Success", Value = appDetails.IsFree ? "Yes" : "No" },
                    new EmbedFieldProperties { Name = "Recommendations", Value = (appDetails.Recommendations?.Total ?? 0).ToString() },
                    new EmbedFieldProperties { Name = "Price", Value = appDetails.PriceOverview?.FinalFormatted ?? "Unknown" },
                    new EmbedFieldProperties { Name = "Released At", Value = releaseDate }
                 ]);

        }

        await Context.Interaction.ModifyResponseAsync(x => x.AddEmbeds(embed));
    }
}