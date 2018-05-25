using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HatServer.Migrations
{
    public partial class NewFieldsForPhrase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClearReviews",
                table: "PhraseItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "PhraseItems",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Packs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Packs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClearReviews",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "PhraseItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Packs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Packs",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
