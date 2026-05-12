using MediatR;
using NetCord;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Configuration;
using SteamCord.Application.Interfaces.Services;

namespace SteamCord.Bot.Features.Steam.Commands;

[SlashCommand("steam", "The steam commands")]
public partial class BaseCommand(SteamSettings steamSettings, ISteamService steamService, ISteamApisService steamApisService, IMediator mediator) : ApplicationCommandModule<ApplicationCommandContext>
{
}