using Microsoft.EntityFrameworkCore.Migrations;
// ReSharper disable All

namespace HatServer.Migrations.GameDb
{
    public partial class AddedGameIdToRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Rounds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Rounds");
        }
    }
}
