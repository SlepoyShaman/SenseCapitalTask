using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenseCapitalTask.Migrations
{
    public partial class updategame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstPlayerTurn",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "fienld",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstPlayerTurn",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "fienld",
                table: "Games");
        }
    }
}
