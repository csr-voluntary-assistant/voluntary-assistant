using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Add_Columns_To_NGO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeadquartersAddress",
                table: "Ongs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Ongs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Ongs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadquartersAddress",
                table: "Ongs");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Ongs");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Ongs");
        }
    }
}
