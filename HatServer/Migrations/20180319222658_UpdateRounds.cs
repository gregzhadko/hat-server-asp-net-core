using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations
{
    public partial class UpdateRounds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_Settings_SettingsId",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_Round_Stages_StageId",
                table: "Round");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Round",
                table: "Round");

            migrationBuilder.RenameTable(
                name: "Round",
                newName: "Rounds");

            migrationBuilder.RenameIndex(
                name: "IX_Round_StageId",
                table: "Rounds",
                newName: "IX_Rounds_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_Round_SettingsId",
                table: "Rounds",
                newName: "IX_Rounds_SettingsId");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "Rounds",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Settings_SettingsId",
                table: "Rounds",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Stages_StageId",
                table: "Rounds",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Settings_SettingsId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Stages_StageId",
                table: "Rounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds");

            migrationBuilder.RenameTable(
                name: "Rounds",
                newName: "Round");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_StageId",
                table: "Round",
                newName: "IX_Round_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_SettingsId",
                table: "Round",
                newName: "IX_Round_SettingsId");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "Round",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Round",
                table: "Round",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Settings_SettingsId",
                table: "Round",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Stages_StageId",
                table: "Round",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
