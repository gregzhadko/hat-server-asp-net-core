using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class AddPhraseItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhraseItemId",
                table: "PhraseStates",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhraseItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Complexity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Phrase = table.Column<string>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhraseItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhraseStates_PhraseItemId",
                table: "PhraseStates",
                column: "PhraseItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates",
                column: "PhraseItemId",
                principalTable: "PhraseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseStates_PhraseItem_PhraseItemId",
                table: "PhraseStates");

            migrationBuilder.DropTable(
                name: "PhraseItem");

            migrationBuilder.DropIndex(
                name: "IX_PhraseStates_PhraseItemId",
                table: "PhraseStates");

            migrationBuilder.DropColumn(
                name: "PhraseItemId",
                table: "PhraseStates");
        }
    }
}
