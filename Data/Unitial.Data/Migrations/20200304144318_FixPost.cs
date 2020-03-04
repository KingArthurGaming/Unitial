using Microsoft.EntityFrameworkCore.Migrations;

namespace Unitial.Data.Migrations
{
    public partial class FixPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveCommets",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "HaveComments",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveComments",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "HaveCommets",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
