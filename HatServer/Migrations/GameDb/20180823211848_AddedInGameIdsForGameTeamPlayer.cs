using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class AddedInGameIdsForGameTeamPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Games",
                newName: "InGameId");

            migrationBuilder.AddColumn<int>(
                name: "InGameId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InGameId",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InGameId",
                table: "GamePhrases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InGameId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "InGameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "InGameId",
                table: "GamePhrases");

            migrationBuilder.RenameColumn(
                name: "InGameId",
                table: "Games",
                newName: "Guid");
        }
    }
}
