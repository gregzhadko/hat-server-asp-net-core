using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HatServer.Data.Migrations
{
    public partial class BindPackAndPhrase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem");

            migrationBuilder.AlterColumn<int>(
                name: "PackId",
                table: "PhraseItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem",
                column: "PackId",
                principalTable: "Pack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem");

            migrationBuilder.AlterColumn<int>(
                name: "PackId",
                table: "PhraseItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem",
                column: "PackId",
                principalTable: "Pack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
