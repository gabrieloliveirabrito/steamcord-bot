using MediatR;
using NetCord;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Features.Steam.Commands;

namespace SteamCord.Bot.Features.Steam.Commands;

[SlashCommand("steam", "The steam commands")]
public partial class BaseCommand(IMediator mediator) : ApplicationCommandModule<ApplicationCommandContext>
{
}