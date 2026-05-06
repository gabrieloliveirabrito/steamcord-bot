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
            await Context.Interaction.SendResponseAsync(InteractionCallback.Message("You can only send this command on guild!"));
            return;
        }

        var userId = Context.User.Id;
        var guildId = Context.Guild!.Id;

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Loading | MessageFlags.Ephemeral));

        var request = new CreateAuthTokenCommand(userId, guildId);
        var response = await mediator.Send(request);

        if (!response.Success)
        {
            await Context.Interaction.ModifyResponseAsync(x => x.Content = response.Error);
            return;
        }

        await Context.Interaction.ModifyResponseAsync(x =>
        {
            x.Content = "Token generated!";
            x.Components = [
                new ActionRowProperties([
                    new ButtonProperties($"steamlink:{response.Token}", "Click here to link your account", ButtonStyle.Primary)
                ])
            ];
        });
        return;
    }
}