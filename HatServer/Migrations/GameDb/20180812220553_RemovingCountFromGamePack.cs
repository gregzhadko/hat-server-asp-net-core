using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class RemovingCountFromGamePack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "GamePacks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "GamePacks",
                nullable: false,
                defaultValue: 0);
        }
    }
}
