using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SubwayEntrance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubwayUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Station = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitute = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubwayUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWithSubways",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    SubwayUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWithSubways", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWithSubways_SubwayUsers_SubwayUserId",
                        column: x => x.SubwayUserId,
                        principalTable: "SubwayUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWithSubways_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWithSubways_SubwayUserId",
                table: "UserWithSubways",
                column: "SubwayUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWithSubways_userId",
                table: "UserWithSubways",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWithSubways");

            migrationBuilder.DropTable(
                name: "SubwayUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
