using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class UpdateVolunteerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Volunteers");

            migrationBuilder.AddColumn<int>(
                name: "RangeInMeters",
                table: "Volunteers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RangeInMeters",
                table: "Volunteers");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Volunteers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Volunteers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
