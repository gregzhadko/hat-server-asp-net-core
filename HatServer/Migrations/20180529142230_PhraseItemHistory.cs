using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class PhraseItemHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhraseItemHistoryId",
                table: "ReviewStates",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhraseItemHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Complexity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Phrase = table.Column<string>(nullable: false),
                    PhraseItemId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhraseItemHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhraseItemHistories_PhraseItems_PhraseItemId",
                        column: x => x.PhraseItemId,
                        principalTable: "PhraseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewStates_PhraseItemHistoryId",
                table: "ReviewStates",
                column: "PhraseItemHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseItemHistories_PhraseItemId",
                table: "PhraseItemHistories",
                column: "PhraseItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewStates_PhraseItemHistories_PhraseItemHistoryId",
                table: "ReviewStates",
                column: "PhraseItemHistoryId",
                principalTable: "PhraseItemHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewStates_PhraseItemHistories_PhraseItemHistoryId",
                table: "ReviewStates");

            migrationBuilder.DropTable(
                name: "PhraseItemHistories");

            migrationBuilder.DropIndex(
                name: "IX_ReviewStates_PhraseItemHistoryId",
                table: "ReviewStates");

            migrationBuilder.DropColumn(
                name: "PhraseItemHistoryId",
                table: "ReviewStates");
        }
    }
}
