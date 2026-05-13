using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SteamCord.Application.Entities;

namespace SteamCord.Infrastructure.Persistence.Configurations;

public class UserGuildConfiguration : IEntityTypeConfiguration<UserGuild>
{
    public void Configure(EntityTypeBuilder<UserGuild> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.UserId, x.GuildConfigId }).IsUnique();

        builder.HasOne(x => x.User).WithMany(x => x.UserGuilds).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);
    }
}