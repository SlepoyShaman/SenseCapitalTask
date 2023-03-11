using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenseCapitalTask.Migrations
{
    public partial class changemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstPlayerId",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecondPlayerId",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SecondPlayerId",
                table: "Games");
        }
    }
}
