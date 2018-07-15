using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HatServer.Migrations.StatisticsDb
{
    public partial class StatisticsContextInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<Guid>(nullable: false),
                    DeviceModel = table.Column<string>(nullable: true),
                    Device = table.Column<string>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    OsVersion = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    PushToken = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packs", x => x.Id);
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
                name: "ServerUser",
                columns: table => new
                {
                    AccessFailedCount = table.Column<int>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerUser", x => x.Id);
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
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "GameUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhraseItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<int>(nullable: false),
                    Phrase = table.Column<string>(nullable: false),
                    Complexity = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PackId = table.Column<int>(nullable: false),
                    TrackId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ClosedById = table.Column<string>(nullable: true),
                    ClosedBy = table.Column<string>(nullable: true),
                    ClosedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhraseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhraseItems_ServerUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "ServerUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhraseItems_Packs_PackId",
                        column: x => x.PackId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Games_GameId",
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
                name: "ReviewState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhraseItemId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewState_PhraseItems_PhraseItemId",
                        column: x => x.PhraseItemId,
                        principalTable: "PhraseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewState_ServerUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ServerUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
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
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<int>(nullable: false),
                    RoundNumber = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    SettingsId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true),
                    StageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rounds_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rounds_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoundPhrases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    PhraseId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundPhrases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundPhrases_PhraseItems_PhraseId",
                        column: x => x.PhraseId,
                        principalTable: "PhraseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundPhrases_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundPhrases_RoundPhraseStates_StateId",
                        column: x => x.StateId,
                        principalTable: "RoundPhraseStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseItems_CreatedById",
                table: "PhraseItems",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseItems_PackId",
                table: "PhraseItems",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewState_PhraseItemId",
                table: "ReviewState",
                column: "PhraseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewState_UserId",
                table: "ReviewState",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrases_PhraseId",
                table: "RoundPhrases",
                column: "PhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrases_RoundId",
                table: "RoundPhrases",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundPhrases_StateId",
                table: "RoundPhrases",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_PlayerId",
                table: "Rounds",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SettingsId",
                table: "Rounds",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_StageId",
                table: "Rounds",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_GameId",
                table: "Stages",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewState");

            migrationBuilder.DropTable(
                name: "RoundPhrases");

            migrationBuilder.DropTable(
                name: "PhraseItems");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "RoundPhraseStates");

            migrationBuilder.DropTable(
                name: "ServerUser");

            migrationBuilder.DropTable(
                name: "Packs");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameUsers");
        }
    }
}
