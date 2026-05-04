using NetCord.Services.ApplicationCommands;

namespace SteamCord.Bot.Features.HelloWorld;

#if DEBUG
public class HelloWorldCommand : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("hello", "Send hello world")]
    public string HelloWorld() => "Hello world!";
}
#endif