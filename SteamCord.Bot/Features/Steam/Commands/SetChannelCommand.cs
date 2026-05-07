using MediatR;
using NetCord;
using NetCord.Services;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Bot.Features.Steam.Commands;

public partial class BaseCommand
{

    [SubSlashCommand("set-channel", "Set the channel that appear logs")]
    [RequireUserPermissions<ApplicationCommandContext>(Permissions.ManageGuild)]
    public async Task SetChannel(
        [SlashCommandParameter(Name = "channel", Description = "The channel that receives the logs")] Channel channel
    )
    {
        var request = new SetSteamChannelCommand(Context.Guild!.Id, channel.Id);
        var result = await mediator.Send(request);

        if (result.Success)
        {
            await Context.Interaction.ModifyResponseAsync(x => x.Content = $"The steam logs channel changed to {channel}");
            return;
        }

        if (!string.IsNullOrEmpty(result.Error))
        {
            await Context.Interaction.ModifyResponseAsync(x => x.Content = result.Error);
            return;
        }

        await Context.Interaction.ModifyResponseAsync(x => x.Content = "Failed to execute set-channel command!");

    }
}