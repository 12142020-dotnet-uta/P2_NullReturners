using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class PasswordAndItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Statistic1",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Statistic2",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Statistic3",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EquipmentItems",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItems", x => x.EquipmentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentItems");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Statistic1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Statistic2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Statistic3",
                table: "Games");
        }
    }
}
