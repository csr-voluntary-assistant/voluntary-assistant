using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Remove_ActionLimit_And_Add_VolunteerStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionLimit",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeInMeters",
                table: "Volunteers",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "RangeInMeters",
                table: "Volunteers",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "ActionLimit",
                table: "AspNetUsers",
                type: "decimal(16,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
