using NetCord.Services.ApplicationCommands;

namespace SteamCord.Bot.Features.HelloWorld;

#if DEBUG
public class HelloWorldCommand : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("hello", "Send hello world")]
    public Task HelloWorld() => Context.Interaction.ModifyResponseAsync(x => x.Content = "Hello World!");
}
#endif