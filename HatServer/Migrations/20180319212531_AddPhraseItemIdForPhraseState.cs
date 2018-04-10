using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class AddPhraseItemIdForPhraseState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates");

            migrationBuilder.AlterColumn<int>(
                name: "PhraseItemId",
                table: "PhraseStates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates",
                column: "PhraseItemId",
                principalTable: "PhraseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates");

            migrationBuilder.AlterColumn<int>(
                name: "PhraseItemId",
                table: "PhraseStates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates",
                column: "PhraseItemId",
                principalTable: "PhraseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
