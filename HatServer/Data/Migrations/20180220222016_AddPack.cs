using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HatServer.Data.Migrations
{
    public partial class AddPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "PhraseItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pack",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pack", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhraseItem_PackId",
                table: "PhraseItem",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem",
                column: "PackId",
                principalTable: "Pack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem");

            migrationBuilder.DropTable(
                name: "Pack");

            migrationBuilder.DropIndex(
                name: "IX_PhraseItem_PackId",
                table: "PhraseItem");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "PhraseItem");
        }
    }
}
