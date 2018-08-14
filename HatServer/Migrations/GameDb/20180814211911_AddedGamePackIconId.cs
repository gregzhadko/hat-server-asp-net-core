using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class AddedGamePackIconId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GamePackIconId",
                table: "GamePacks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamePackIconId",
                table: "GamePacks");
        }
    }
}
