﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class AddedConnectionBetweenDeviceAndGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Games",
                newName: "DeviceInfoGuid");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "DeviceInfos",
                newName: "DeviceGuid");

            migrationBuilder.AddColumn<int>(
                name: "DeviceInfoId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeviceInfoId",
                table: "Games",
                column: "DeviceInfoId");

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

            migrationBuilder.DropIndex(
                name: "IX_Games_DeviceInfoId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DeviceInfoId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "DeviceInfoGuid",
                table: "Games",
                newName: "DeviceId");

            migrationBuilder.RenameColumn(
                name: "DeviceGuid",
                table: "DeviceInfos",
                newName: "DeviceId");
        }
    }
}
