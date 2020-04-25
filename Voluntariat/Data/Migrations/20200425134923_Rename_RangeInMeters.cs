using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Rename_RangeInMeters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RangeInMeters",
                table: "Volunteers");

            migrationBuilder.AddColumn<decimal>(
                name: "RangeInKm",
                table: "AspNetUsers",
                type: "decimal(16,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RangeInKm",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<decimal>(
                name: "RangeInMeters",
                table: "Volunteers",
                type: "decimal(16,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
