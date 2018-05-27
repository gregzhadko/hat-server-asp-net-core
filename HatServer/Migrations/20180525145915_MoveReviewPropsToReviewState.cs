using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class MoveReviewPropsToReviewState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClearReviews",
                table: "PhraseItems");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "PhraseItems");

            migrationBuilder.AddColumn<bool>(
                name: "ClearReviews",
                table: "ReviewStates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ReviewStates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClearReviews",
                table: "ReviewStates");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ReviewStates");

            migrationBuilder.AddColumn<bool>(
                name: "ClearReviews",
                table: "PhraseItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "PhraseItems",
                nullable: true);
        }
    }
}
