using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Rename_ONG_References_To_NGO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ongs");

            migrationBuilder.DropColumn(
                name: "ActivateNotificationsFromOtherOngs",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "OngID",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "OngID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OngID",
                table: "Beneficiaries");

            migrationBuilder.AddColumn<bool>(
                name: "ActivateNotificationsFromOtherNGOs",
                table: "Volunteers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "NGOID",
                table: "Volunteers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NGOID",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NGOID",
                table: "Beneficiaries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NGOs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NGOStatus = table.Column<int>(nullable: false),
                    CreatedByID = table.Column<Guid>(nullable: false),
                    HeadquartersAddress = table.Column<string>(nullable: false),
                    HeadquartersAddressLatitude = table.Column<double>(nullable: false),
                    HeadquartersAddressLongitude = table.Column<double>(nullable: false),
                    HeadquartersEmail = table.Column<string>(nullable: false),
                    HeadquartersPhoneNumber = table.Column<string>(nullable: false),
                    DialingCode = table.Column<int>(nullable: false),
                    IdentificationNumber = table.Column<string>(nullable: false),
                    Website = table.Column<string>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false),
                    ServiceID = table.Column<Guid>(nullable: false),
                    FileIDs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGOs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NGOs");

            migrationBuilder.DropColumn(
                name: "ActivateNotificationsFromOtherNGOs",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "NGOID",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "NGOID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NGOID",
                table: "Beneficiaries");

            migrationBuilder.AddColumn<bool>(
                name: "ActivateNotificationsFromOtherOngs",
                table: "Volunteers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OngID",
                table: "Volunteers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OngID",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OngID",
                table: "Beneficiaries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Ongs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DialingCode = table.Column<int>(type: "int", nullable: false),
                    FileIDs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadquartersAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersAddressLatitude = table.Column<double>(type: "float", nullable: false),
                    HeadquartersAddressLongitude = table.Column<double>(type: "float", nullable: false),
                    HeadquartersEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquartersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ongs", x => x.ID);
                });
        }
    }
}
