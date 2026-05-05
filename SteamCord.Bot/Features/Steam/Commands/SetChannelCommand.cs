using MediatR;
using NetCord;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Bot.Features.Steam.Commands;

public partial class BaseCommand
{

    [SubSlashCommand("set-channel", "Set the channel that appear logs")]
    public async Task<string> SetChannel(
        [SlashCommandParameter(Name ="channel", Description = "The channel that receives the logs")] Channel channel
    )
    {
        var request = new SetSteamChannelCommand(Context.Guild!.Id, channel.Id);
        var result = await mediator.Send(request);

        if(result.Success)
        {
            return $"The steam logs channel changed to {channel}";
        }
        else if(!string.IsNullOrEmpty(result.Error))
        {
            return result.Error;
        }

        return "Failed to execute set-channel command!";
    }
}