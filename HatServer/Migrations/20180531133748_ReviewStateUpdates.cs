using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class ReviewStateUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewStates_AspNetUsers_UserId",
                table: "ReviewStates");

            migrationBuilder.DropColumn(
                name: "ClearReviews",
                table: "ReviewStates");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReviewStates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewStates_AspNetUsers_UserId",
                table: "ReviewStates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewStates_AspNetUsers_UserId",
                table: "ReviewStates");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReviewStates",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "ClearReviews",
                table: "ReviewStates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewStates_AspNetUsers_UserId",
                table: "ReviewStates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
