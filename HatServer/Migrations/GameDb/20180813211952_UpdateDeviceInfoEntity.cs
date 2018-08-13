using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class UpdateDeviceInfoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameUsers_UserId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Os",
                table: "GameUsers",
                newName: "OsName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Games",
                newName: "DeviceInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_UserId",
                table: "Games",
                newName: "IX_Games_DeviceInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameUsers_DeviceInfoId",
                table: "Games",
                column: "DeviceInfoId",
                principalTable: "GameUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameUsers_DeviceInfoId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "OsName",
                table: "GameUsers",
                newName: "Os");

            migrationBuilder.RenameColumn(
                name: "DeviceInfoId",
                table: "Games",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_DeviceInfoId",
                table: "Games",
                newName: "IX_Games_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameUsers_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "GameUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
