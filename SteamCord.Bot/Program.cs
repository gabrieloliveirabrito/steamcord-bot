using System.Text;
using DotEnv.Core;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Hosting.Services.Commands;
using NetCord.Hosting.Services.ComponentInteractions;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;
using Serilog;
using Serilog.Events;
using SteamCord.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, options) =>
{
#if DEBUG
    options.MinimumLevel.Verbose();
#else
    options.MinimumLevel.Information();
    options.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
#endif
    options.Enrich.FromLogContext();
    options.WriteTo.Console();
});

var envPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath!, "../.env"));
builder.Services.AddDotEnv<AppSettings>(envPath);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddDiscordGateway((options, services) =>
{
    var configuration = services.GetRequiredService<AppSettings>();

    options.Token = configuration.DiscordToken;
    options.PublicKey = configuration.DiscordPublicKey;
    options.AutoStartStop = true;
    options.Intents = GatewayIntents.Guilds |
    GatewayIntents.GuildMessages |
    GatewayIntents.DirectMessages |
    GatewayIntents.MessageContent |
    GatewayIntents.DirectMessageReactions |
    GatewayIntents.GuildMessageReactions;

    options.Presence = new PresenceProperties(UserStatusType.Online).WithActivities([
        new UserActivityProperties("Logging Steam", UserActivityType.Playing)
    ]);
});

builder.Services.AddApplicationCommands(options =>
{
    options.AutoRegisterCommands = true;
});

builder.Services.AddCommands(options =>
{
    options.Prefix = "!";
});

builder.Services
    .AddComponentInteractions<ButtonInteraction, ButtonInteractionContext>()
    .AddComponentInteractions<StringMenuInteraction, StringMenuInteractionContext>()
    .AddComponentInteractions<UserMenuInteraction, UserMenuInteractionContext>()
    .AddComponentInteractions<RoleMenuInteraction, RoleMenuInteractionContext>()
    .AddComponentInteractions<MentionableMenuInteraction, MentionableMenuInteractionContext>()
    .AddComponentInteractions<ChannelMenuInteraction, ChannelMenuInteractionContext>()
    .AddComponentInteractions<ModalInteraction, ModalInteractionContext>();

builder.Services.AddGatewayHandlers(typeof(Program).Assembly);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.AddModules(typeof(Program).Assembly);
app.UseHttpsRedirection();

app.Run();