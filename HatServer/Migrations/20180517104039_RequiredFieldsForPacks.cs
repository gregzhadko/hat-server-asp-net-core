using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class RequiredFieldsForPacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
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
                name: "Author",
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
