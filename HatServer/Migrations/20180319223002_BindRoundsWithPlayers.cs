using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class BindRoundsWithPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Rounds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_PlayerId",
                table: "Rounds",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Players_PlayerId",
                table: "Rounds",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Players_PlayerId",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_PlayerId",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Rounds");
        }
    }
}
