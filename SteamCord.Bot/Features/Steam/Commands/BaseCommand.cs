using MediatR;
using NetCord;
using NetCord.Services.ApplicationCommands;
using SteamCord.Application.Configuration;
using SteamCord.Application.Features.Steam.Commands;
using SteamCord.Application.Interfaces;

namespace SteamCord.Bot.Features.Steam.Commands;

[SlashCommand("steam", "The steam commands")]
public partial class BaseCommand(SteamSettings steamSettings, ISteamService steamService, IMediator mediator) : ApplicationCommandModule<ApplicationCommandContext>
{
}