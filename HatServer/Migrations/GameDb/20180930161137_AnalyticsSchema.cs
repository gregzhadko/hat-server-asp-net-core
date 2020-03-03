using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
// ReSharper disable All

namespace HatServer.Migrations.GameDb
{
    public partial class AnalyticsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceGuid = table.Column<Guid>(nullable: false),
                    DeviceModel = table.Column<string>(nullable: true),
                    Device = table.Column<string>(nullable: true),
                    OsName = table.Column<string>(nullable: true),
                    OsVersion = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    PushToken = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    GamePackIconId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InGameId = table.Column<string>(nullable: true),
                    DeviceId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoundPhraseStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundPhraseStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CanChangeWord = table.Column<bool>(nullable: false),
                    RoundTime = table.Column<int>(nullable: false),
                    BadItalicSimulated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DownloadedPacksInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<Guid>(nullable: false),
                    GamePackId = table.Column<int>(nullable: false),
                    DownloadedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadedPacksInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadedPacksInfos_GamePacks_GamePackId",
                        column: x => x.GamePackId,
                        principalTable: "GamePacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePackIcons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Icon = table.Column<byte[]>(nullable: true),
                    GamePackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePackIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePackIcons_GamePacks_GamePackId",
                        column: x => x.GamePackId,
                        principalTable: "GamePacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePhrases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Phrase = table.Column<string>(nullable: false),
                    Complexity = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GamePackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePhrases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePhrases_GamePacks_GamePackId",
                        column: x => x.GamePackId,
                        principalTable: "GamePacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InGamePhrase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InGameId = table.Column<int>(nullable: false),
                    Word = table.Column<string>(nullable: true),
                    BadItalic = table.Column<bool>(nullable: false),
                    PackId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGamePhrase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InGamePhrase_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    InGameId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<string>(nullable: true),
                    RoundNumber = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    DeviceId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Stage = table.Column<int>(nullable: false),
                    SettingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    InGameId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundPhrases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false),
                    WordId = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundPhrases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundPhrases_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadedPacksInfos_GamePackId",
                table: "DownloadedPacksInfos",
                column: "GamePackId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePackIcons_GamePackId",
                table: "GamePackIcons",
                column: "GamePackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamePhrases_GamePackId",
                table: "GamePhrases",
                column: "GamePackId");

            migrationBuilder.CreateIndex(
                name: "IX_InGamePhrase_GameId",
                table: "InGamePhrase",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrases_RoundId",
                table: "RoundPhrases",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SettingsId",
                table: "Rounds",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceInfos");

            migrationBuilder.DropTable(
                name: "DownloadedPacksInfos");

            migrationBuilder.DropTable(
                name: "GamePackIcons");

            migrationBuilder.DropTable(
                name: "GamePhrases");

            migrationBuilder.DropTable(
                name: "InGamePhrase");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "RoundPhrases");

            migrationBuilder.DropTable(
                name: "RoundPhraseStates");

            migrationBuilder.DropTable(
                name: "GamePacks");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
