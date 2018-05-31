using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HatServer.Migrations
{
    public partial class ComplexityIsDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Complexity",
                table: "PhraseItems",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Complexity",
                table: "PhraseItems",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
