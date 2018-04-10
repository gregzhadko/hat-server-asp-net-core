using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HatServer.Migrations
{
    public partial class AddGameUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Device = table.Column<string>(nullable: true),
                    DeviceId = table.Column<Guid>(nullable: false),
                    DeviceModel = table.Column<string>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    OsVersion = table.Column<string>(nullable: true),
                    PushToken = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<int>(nullable: false),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUsers");
        }
    }
}
