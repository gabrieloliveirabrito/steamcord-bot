using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Bot.Features.Steam.Commands;

public partial class BaseCommand
{
    [SubSlashCommand("connect", "Connect your steam account to bot")]
    public async Task Connect()
    {
        if (Context.Guild is null)
        {
            await Context.Interaction.ModifyResponseAsync(x => x.Content = "You can only send this command on guild!");
            return;
        }

        var userId = Context.User.Id;
        var guildId = Context.Guild!.Id;

        var token = await CreateAuthToken(userId, guildId);
        if (token is null)
        {
            return;
        }

        var url = string.Format(steamSettings.AuthUrl, token);

        await Context.Interaction.ModifyResponseAsync(x =>
        {
            x.Content = "Token generated!";
            x.Components = [
                new ActionRowProperties([
                    new LinkButtonProperties(url, "Click here to link your account", EmojiProperties.Standard("🔗"))
                ])
            ];
        });
        return;
    }

    async Task<string?> CreateAuthToken(ulong userId, ulong guildId)
    {
        var request = new CreateAuthTokenCommand(userId, guildId);
        var response = await mediator.Send(request);

        if (!response.Success)
        {
            await Context.Interaction.ModifyResponseAsync(x => x.Content = response.Error);
            return null;
        }
        else
        {
            return response.Token;
        }
    }
}