using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HatServer.Migrations
{
    public partial class AddPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhraseItem",
                table: "PhraseItem");

            migrationBuilder.RenameTable(
                name: "PhraseItem",
                newName: "PhraseItems");

            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "PhraseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhraseItems",
                table: "PhraseItems",
                column: "Id");

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
                name: "IX_PhraseItems_PackId",
                table: "PhraseItems",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItems_Pack_PackId",
                table: "PhraseItems",
                column: "PackId",
                principalTable: "Pack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseStates_PhraseItems_PhraseItemId",
                table: "PhraseStates",
                column: "PhraseItemId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItems_Pack_PackId",
                table: "PhraseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PhraseStates_PhraseItems_PhraseItemId",
                table: "PhraseStates");

            migrationBuilder.DropTable(
                name: "Pack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhraseItems",
                table: "PhraseItems");

            migrationBuilder.DropIndex(
                name: "IX_PhraseItems_PackId",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "PhraseItems");

            migrationBuilder.RenameTable(
                name: "PhraseItems",
                newName: "PhraseItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhraseItem",
                table: "PhraseItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates",
                column: "PhraseItemId",
                principalTable: "PhraseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
