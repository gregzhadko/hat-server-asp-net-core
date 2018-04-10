using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class AddSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItems_Pack_PackId",
                table: "PhraseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pack",
                table: "Pack");

            migrationBuilder.RenameTable(
                name: "Pack",
                newName: "Packs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packs",
                table: "Packs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItems_Packs_PackId",
                table: "PhraseItems",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItems_Packs_PackId",
                table: "PhraseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packs",
                table: "Packs");

            migrationBuilder.RenameTable(
                name: "Packs",
                newName: "Pack");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pack",
                table: "Pack",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItems_Pack_PackId",
                table: "PhraseItems",
                column: "PackId",
                principalTable: "Pack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
