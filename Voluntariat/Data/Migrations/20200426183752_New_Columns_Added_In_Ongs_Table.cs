using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class New_Columns_Added_In_Ongs_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DialingCode",
                table: "Ongs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "HeadquartersAddressLatitude",
                table: "Ongs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HeadquartersAddressLongitude",
                table: "Ongs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "HeadquartersEmail",
                table: "Ongs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeadquartersPhoneNumber",
                table: "Ongs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DialingCode",
                table: "Ongs");

            migrationBuilder.DropColumn(
                name: "HeadquartersAddressLatitude",
                table: "Ongs");

            migrationBuilder.DropColumn(
                name: "HeadquartersAddressLongitude",
                table: "Ongs");

            migrationBuilder.DropColumn(
                name: "HeadquartersEmail",
                table: "Ongs");

            migrationBuilder.DropColumn(
                name: "HeadquartersPhoneNumber",
                table: "Ongs");
        }
    }
}
