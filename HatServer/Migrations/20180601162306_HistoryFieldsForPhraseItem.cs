using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HatServer.Migrations
{
    public partial class HistoryFieldsForPhraseItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClosedBy",
                table: "PhraseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClosedById",
                table: "PhraseItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "PhraseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "PhraseItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PhraseItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "PhraseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhraseItems_CreatedById",
                table: "PhraseItems",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PhraseItems_AspNetUsers_CreatedById",
                table: "PhraseItems",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhraseItems_AspNetUsers_CreatedById",
                table: "PhraseItems");

            migrationBuilder.DropIndex(
                name: "IX_PhraseItems_CreatedById",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "ClosedBy",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "ClosedById",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "PhraseItems");
        }
    }
}
