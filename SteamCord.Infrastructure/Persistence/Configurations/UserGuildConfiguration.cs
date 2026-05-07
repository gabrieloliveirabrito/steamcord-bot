using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SteamCord.Application.Entities;

namespace SteamCord.Infrastructure.Persistence.Configurations;

public class UserGuildConfiguration : IEntityTypeConfiguration<UserGuild>
{
    public void Configure(EntityTypeBuilder<UserGuild> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.UserId, x.GuildId }).IsUnique();
    }
}