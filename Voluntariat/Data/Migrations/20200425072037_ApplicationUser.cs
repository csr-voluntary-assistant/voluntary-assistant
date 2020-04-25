using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ActionLimit",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "HasDriverLicence",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherTransportationMethod",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportationMethod",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasDriverLicence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtherTransportationMethod",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TransportationMethod",
                table: "AspNetUsers");
        }
    }
}
