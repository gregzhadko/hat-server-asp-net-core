using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HatServer.Data.Migrations
{
    public partial class updateState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PhraseItem",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "PhraseItem");
        }
    }
}
