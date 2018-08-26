using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class NullableDeviceIdKeyForGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DeviceInfos_DeviceInfoId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceInfoId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DeviceInfos_DeviceInfoId",
                table: "Games",
                column: "DeviceInfoId",
                principalTable: "DeviceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DeviceInfos_DeviceInfoId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceInfoId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DeviceInfos_DeviceInfoId",
                table: "Games",
                column: "DeviceInfoId",
                principalTable: "DeviceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
