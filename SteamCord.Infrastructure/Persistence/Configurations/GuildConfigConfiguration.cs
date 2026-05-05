using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SteamCord.Infrastructure.Persistence.Entities;

namespace SteamCord.Infrastructure.Persistence.Configurations;

public class GuildConfigConfiguration : IEntityTypeConfiguration<GuildConfig>
{
    public void Configure(EntityTypeBuilder<GuildConfig> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.GuildId).IsUnique();
    }
}