using Microsoft.EntityFrameworkCore.Migrations;
// ReSharper disable All

namespace HatServer.Migrations.GameDb
{
    public partial class RenameGameIdToGameGUIDInRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Rounds",
                newName: "GameGUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameGUID",
                table: "Rounds",
                newName: "GameId");
        }
    }
}
