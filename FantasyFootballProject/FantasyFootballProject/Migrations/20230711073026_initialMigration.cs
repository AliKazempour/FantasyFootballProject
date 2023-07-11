using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootballProject.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    teamCode = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.teamCode);
                });

            migrationBuilder.CreateTable(
                name: "temp_users",
                columns: table => new
                {
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", nullable: false),
                    time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp_users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    playerKey = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    second_name = table.Column<string>(type: "TEXT", nullable: false),
                    element_type = table.Column<int>(type: "INTEGER", nullable: false),
                    now_cost = table.Column<int>(type: "INTEGER", nullable: false),
                    team = table.Column<int>(type: "INTEGER", nullable: false),
                    total_points = table.Column<double>(type: "REAL", nullable: false),
                    teamCode = table.Column<int>(type: "INTEGER", nullable: true),
                    teamCode1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.playerKey);
                    table.ForeignKey(
                        name: "FK_players_Team_teamCode",
                        column: x => x.teamCode,
                        principalTable: "Team",
                        principalColumn: "teamCode");
                    table.ForeignKey(
                        name: "FK_players_Team_teamCode1",
                        column: x => x.teamCode1,
                        principalTable: "Team",
                        principalColumn: "teamCode");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Family = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    verified = table.Column<bool>(type: "INTEGER", nullable: false),
                    score = table.Column<double>(type: "REAL", nullable: false),
                    money = table.Column<int>(type: "INTEGER", nullable: false),
                    teamCode = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_Team_teamCode",
                        column: x => x.teamCode,
                        principalTable: "Team",
                        principalColumn: "teamCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_players_teamCode",
                table: "players",
                column: "teamCode");

            migrationBuilder.CreateIndex(
                name: "IX_players_teamCode1",
                table: "players",
                column: "teamCode1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_teamCode",
                table: "Users",
                column: "teamCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "temp_users");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
