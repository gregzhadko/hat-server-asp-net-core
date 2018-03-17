using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HatServer.Migrations
{
    public partial class UpdatePhraseItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseItemId",
                table: "RoundPhrases");

            migrationBuilder.DropIndex(
                name: "IX_RoundPhrases_PhraseItemId",
                table: "RoundPhrases");

            migrationBuilder.DropColumn(
                name: "PhraseItemId",
                table: "RoundPhrases");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrases_PhraseId",
                table: "RoundPhrases",
                column: "PhraseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseId",
                table: "RoundPhrases",
                column: "PhraseId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseId",
                table: "RoundPhrases");

            migrationBuilder.DropIndex(
                name: "IX_RoundPhrases_PhraseId",
                table: "RoundPhrases");

            migrationBuilder.AddColumn<int>(
                name: "PhraseItemId",
                table: "RoundPhrases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrases_PhraseItemId",
                table: "RoundPhrases",
                column: "PhraseItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPhrases_PhraseItems_PhraseItemId",
                table: "RoundPhrases",
                column: "PhraseItemId",
                principalTable: "PhraseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
