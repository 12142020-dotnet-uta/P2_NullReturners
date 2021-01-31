using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class DateToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
           

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamID = table.Column<int>(type: "int", nullable: false),
                    AwayTeamID = table.Column<int>(type: "int", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinningTeam = table.Column<int>(type: "int", nullable: false),
                    HomeScore = table.Column<int>(type: "int", nullable: false),
                    AwayScore = table.Column<int>(type: "int", nullable: false),
                    Statistic1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statistic2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statistic3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
