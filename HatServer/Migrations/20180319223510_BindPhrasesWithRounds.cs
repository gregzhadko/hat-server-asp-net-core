using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HatServer.Migrations
{
    public partial class BindPhrasesWithRounds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoundPhraseState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundPhraseState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoundPhrase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhraseId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundPhrase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundPhrase_PhraseItems_PhraseId",
                        column: x => x.PhraseId,
                        principalTable: "PhraseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundPhrase_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundPhrase_RoundPhraseState_StateId",
                        column: x => x.StateId,
                        principalTable: "RoundPhraseState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrase_PhraseId",
                table: "RoundPhrase",
                column: "PhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrase_RoundId",
                table: "RoundPhrase",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrase_StateId",
                table: "RoundPhrase",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoundPhrase");

            migrationBuilder.DropTable(
                name: "RoundPhraseState");
        }
    }
}
