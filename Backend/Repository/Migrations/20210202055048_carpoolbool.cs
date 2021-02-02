using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class carpoolbool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCarpool",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCarpool",
                table: "Messages");
        }
    }
}
