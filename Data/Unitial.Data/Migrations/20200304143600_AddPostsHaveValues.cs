using Microsoft.EntityFrameworkCore.Migrations;

namespace Unitial.Data.Migrations
{
    public partial class AddPostsHaveValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveCommets",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HaveLikes",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveCommets",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "HaveLikes",
                table: "Posts");
        }
    }
}
