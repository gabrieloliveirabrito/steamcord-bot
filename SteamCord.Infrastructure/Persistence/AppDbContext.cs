using Microsoft.EntityFrameworkCore;
using SteamCord.Application.Entities;

namespace SteamCord.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<GuildConfig> GuildConfigs => Set<GuildConfig>();
    public DbSet<UserGuild> UserGuilds => Set<UserGuild>();
    public DbSet<UserToken> UserTokens => Set<UserToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}