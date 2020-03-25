using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unitial.Data.Migrations
{
    public partial class rebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_IsDeleted",
                table: "Posts",
                column: "IsDeleted");
        }
    }
}
