using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.StatisticsDb
{
    public partial class VersionAndFreePropertiesForPacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItems_ServerUser_CreatedById",
                table: "PhraseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItems_Packs_PackId",
                table: "PhraseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewState_PhraseItems_PhraseItemId",
                table: "ReviewState");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseId",
                table: "RoundPhrases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhraseItems",
                table: "PhraseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packs",
                table: "Packs");

            migrationBuilder.RenameTable(
                name: "PhraseItems",
                newName: "PhraseItem");

            migrationBuilder.RenameTable(
                name: "Packs",
                newName: "Pack");

            migrationBuilder.RenameIndex(
                name: "IX_PhraseItems_PackId",
                table: "PhraseItem",
                newName: "IX_PhraseItem_PackId");

            migrationBuilder.RenameIndex(
                name: "IX_PhraseItems_CreatedById",
                table: "PhraseItem",
                newName: "IX_PhraseItem_CreatedById");

            migrationBuilder.AddColumn<bool>(
                name: "Free",
                table: "Pack",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Pack",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhraseItem",
                table: "PhraseItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pack",
                table: "Pack",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProdPacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<int>(nullable: false),
                    Free = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdPacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdPhraseItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Phrase = table.Column<string>(nullable: false),
                    Complexity = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProdPackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdPhraseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdPhraseItems_ProdPacks_ProdPackId",
                        column: x => x.ProdPackId,
                        principalTable: "ProdPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdPhraseItems_ProdPackId",
                table: "ProdPhraseItems",
                column: "ProdPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItem_ServerUser_CreatedById",
                table: "PhraseItem",
                column: "CreatedById",
                principalTable: "ServerUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem",
                column: "PackId",
                principalTable: "Pack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewState_PhraseItem_PhraseItemId",
                table: "ReviewState",
                column: "PhraseItemId",
                principalTable: "PhraseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_PhraseItem_PhraseId",
                table: "RoundPhrases",
                column: "PhraseId",
                principalTable: "PhraseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItem_ServerUser_CreatedById",
                table: "PhraseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItem_Pack_PackId",
                table: "PhraseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewState_PhraseItem_PhraseItemId",
                table: "ReviewState");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_PhraseItem_PhraseId",
                table: "RoundPhrases");

            migrationBuilder.DropTable(
                name: "ProdPhraseItems");

            migrationBuilder.DropTable(
                name: "ProdPacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhraseItem",
                table: "PhraseItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pack",
                table: "Pack");

            migrationBuilder.DropColumn(
                name: "Free",
                table: "Pack");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Pack");

            migrationBuilder.RenameTable(
                name: "PhraseItem",
                newName: "PhraseItems");

            migrationBuilder.RenameTable(
                name: "Pack",
                newName: "Packs");

            migrationBuilder.RenameIndex(
                name: "IX_PhraseItem_PackId",
                table: "PhraseItems",
                newName: "IX_PhraseItems_PackId");

            migrationBuilder.RenameIndex(
                name: "IX_PhraseItem_CreatedById",
                table: "PhraseItems",
                newName: "IX_PhraseItems_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhraseItems",
                table: "PhraseItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packs",
                table: "Packs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItems_ServerUser_CreatedById",
                table: "PhraseItems",
                column: "CreatedById",
                principalTable: "ServerUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItems_Packs_PackId",
                table: "PhraseItems",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewState_PhraseItems_PhraseItemId",
                table: "ReviewState",
                column: "PhraseItemId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseId",
                table: "RoundPhrases",
                column: "PhraseId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
