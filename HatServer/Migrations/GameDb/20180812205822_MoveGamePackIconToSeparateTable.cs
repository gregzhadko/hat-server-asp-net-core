using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.GameDb
{
    public partial class MoveGamePackIconToSeparateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "GamePacks");

            migrationBuilder.CreateTable(
                name: "GamePackIcons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Icon = table.Column<byte[]>(nullable: true),
                    GamePackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePackIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePackIcons_GamePacks_GamePackId",
                        column: x => x.GamePackId,
                        principalTable: "GamePacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePackIcons_GamePackId",
                table: "GamePackIcons",
                column: "GamePackId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePackIcons");

            migrationBuilder.AddColumn<byte[]>(
                name: "Icon",
                table: "GamePacks",
                nullable: true);
        }
    }
}
