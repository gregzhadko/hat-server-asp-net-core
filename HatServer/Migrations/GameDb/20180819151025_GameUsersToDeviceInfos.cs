using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class GameUsersToDeviceInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameUsers_DeviceInfoId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameUsers",
                table: "GameUsers");

            migrationBuilder.RenameTable(
                name: "GameUsers",
                newName: "DeviceInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceInfos",
                table: "DeviceInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DeviceInfos_DeviceInfoId",
                table: "Games",
                column: "DeviceInfoId",
                principalTable: "DeviceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DeviceInfos_DeviceInfoId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceInfos",
                table: "DeviceInfos");

            migrationBuilder.RenameTable(
                name: "DeviceInfos",
                newName: "GameUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameUsers",
                table: "GameUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameUsers_DeviceInfoId",
                table: "Games",
                column: "DeviceInfoId",
                principalTable: "GameUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
