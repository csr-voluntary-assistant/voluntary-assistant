using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Move_Column_RangeInKm_In_Users_Table_And_Add_VolunteerStatus_Column_To_Volunteer_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RangeInMeters",
                table: "Volunteers");

            migrationBuilder.RenameColumn(
                name: "ActionLimit",
                table: "AspNetUsers",
                newName: "RangeInKm");

            migrationBuilder.AddColumn<int>(
                name: "VolunteerStatus",
                table: "Volunteers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerStatus",
                table: "Volunteers");

            migrationBuilder.RenameColumn(
                name: "RangeInKm",
                table: "AspNetUsers",
                newName: "ActionLimit");

            migrationBuilder.AddColumn<int>(
                name: "RangeInMeters",
                table: "Volunteers",
                type: "int",
                nullable: true);
        }
    }
}
