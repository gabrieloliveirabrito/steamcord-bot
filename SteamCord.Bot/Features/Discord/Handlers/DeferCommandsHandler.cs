using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace SteamCord.Bot.Features.Discord.Handlers;

public class DeferCommandsHandler : IInteractionCreateGatewayHandler
{
    public async ValueTask HandleAsync(Interaction interaction)
    {
        if (interaction is ApplicationCommandInteraction)
        {
            await interaction.SendResponseAsync(
                InteractionCallback.DeferredMessage(MessageFlags.Loading | MessageFlags.Ephemeral)
            );
        }
    }
}