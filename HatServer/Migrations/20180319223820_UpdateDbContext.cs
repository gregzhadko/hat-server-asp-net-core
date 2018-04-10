using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class UpdateDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrase_PhraseItems_PhraseId",
                table: "RoundPhrase");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrase_Rounds_RoundId",
                table: "RoundPhrase");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrase_RoundPhraseState_StateId",
                table: "RoundPhrase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundPhraseState",
                table: "RoundPhraseState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundPhrase",
                table: "RoundPhrase");

            migrationBuilder.RenameTable(
                name: "RoundPhraseState",
                newName: "RoundPhraseStates");

            migrationBuilder.RenameTable(
                name: "RoundPhrase",
                newName: "RoundPhrases");

            migrationBuilder.RenameIndex(
                name: "IX_RoundPhrase_StateId",
                table: "RoundPhrases",
                newName: "IX_RoundPhrases_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundPhrase_RoundId",
                table: "RoundPhrases",
                newName: "IX_RoundPhrases_RoundId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundPhrase_PhraseId",
                table: "RoundPhrases",
                newName: "IX_RoundPhrases_PhraseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundPhraseStates",
                table: "RoundPhraseStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundPhrases",
                table: "RoundPhrases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseId",
                table: "RoundPhrases",
                column: "PhraseId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_Rounds_RoundId",
                table: "RoundPhrases",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_RoundPhraseStates_StateId",
                table: "RoundPhrases",
                column: "StateId",
                principalTable: "RoundPhraseStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseId",
                table: "RoundPhrases");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_Rounds_RoundId",
                table: "RoundPhrases");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_RoundPhraseStates_StateId",
                table: "RoundPhrases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundPhraseStates",
                table: "RoundPhraseStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundPhrases",
                table: "RoundPhrases");

            migrationBuilder.RenameTable(
                name: "RoundPhraseStates",
                newName: "RoundPhraseState");

            migrationBuilder.RenameTable(
                name: "RoundPhrases",
                newName: "RoundPhrase");

            migrationBuilder.RenameIndex(
                name: "IX_RoundPhrases_StateId",
                table: "RoundPhrase",
                newName: "IX_RoundPhrase_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundPhrases_RoundId",
                table: "RoundPhrase",
                newName: "IX_RoundPhrase_RoundId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundPhrases_PhraseId",
                table: "RoundPhrase",
                newName: "IX_RoundPhrase_PhraseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundPhraseState",
                table: "RoundPhraseState",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundPhrase",
                table: "RoundPhrase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrase_PhraseItems_PhraseId",
                table: "RoundPhrase",
                column: "PhraseId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrase_Rounds_RoundId",
                table: "RoundPhrase",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrase_RoundPhraseState_StateId",
                table: "RoundPhrase",
                column: "StateId",
                principalTable: "RoundPhraseState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
