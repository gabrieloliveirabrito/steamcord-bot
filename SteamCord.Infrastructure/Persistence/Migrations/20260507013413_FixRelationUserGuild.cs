using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamCord.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationUserGuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGuilds_GuildConfigs_GuildConfigId",
                table: "UserGuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGuilds_Users_UserId",
                table: "UserGuilds");

            migrationBuilder.DropIndex(
                name: "IX_UserGuilds_GuildConfigId",
                table: "UserGuilds");

            migrationBuilder.DropIndex(
                name: "IX_UserGuilds_UserId",
                table: "UserGuilds");

            migrationBuilder.DropColumn(
                name: "GuildConfigId",
                table: "UserGuilds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuildConfigId",
                table: "UserGuilds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserGuilds_GuildConfigId",
                table: "UserGuilds",
                column: "GuildConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGuilds_UserId",
                table: "UserGuilds",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGuilds_GuildConfigs_GuildConfigId",
                table: "UserGuilds",
                column: "GuildConfigId",
                principalTable: "GuildConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGuilds_Users_UserId",
                table: "UserGuilds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
