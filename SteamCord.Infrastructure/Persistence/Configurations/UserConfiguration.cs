using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SteamCord.Application.Entities;

namespace SteamCord.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.DiscordId).IsUnique();

        builder.HasMany(x => x.UserGuilds).WithOne(x => x.User).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);
    }
}