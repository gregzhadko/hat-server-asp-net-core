using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class AddedDeviceIdForGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "Games",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Games");
        }
    }
}
