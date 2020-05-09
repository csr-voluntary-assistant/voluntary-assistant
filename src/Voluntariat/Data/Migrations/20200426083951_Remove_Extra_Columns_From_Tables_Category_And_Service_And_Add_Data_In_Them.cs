using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Remove_Extra_Columns_From_Tables_Category_And_Service_And_Add_Data_In_Them : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeneficiariesCount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NGOsCount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProjectsCount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "VolunteersCount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BeneficiariesCount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NGOsCount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProjectsCount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "VolunteersCount",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "AddedBy", "CategoryStatus", "CreatedOn", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4817ee60-a647-400f-bf3a-265fe184fe81"), 0, 1, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seniori 65+", "Seniori 65+" },
                    { new Guid("b6619b80-4fb7-48a8-8a2e-9383afbdff93"), 0, 1, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Persoane cu dizabilitați", "Persoane cu dizabilitați" },
                    { new Guid("887e9957-4f9b-4b13-878d-643c5040c19e"), 0, 1, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Persoane autoizolate", "Persoane autoizolate" },
                    { new Guid("b285dbbc-6696-4dbd-80ff-795c83c13c42"), 0, 1, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cazuri sociale", "Cazuri sociale" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ID", "AddedBy", "CreatedOn", "Description", "Name", "ServiceStatus" },
                values: new object[,]
                {
                    { new Guid("1261ee94-293e-4a4d-99f6-5e4f42451093"), 0, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hrana si stricta necesitate", "Hrana si stricta necesitate", 1 },
                    { new Guid("b7fca1ba-0899-41f0-950a-9bfe2673c931"), 0, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medicamente", "Medicamente", 1 },
                    { new Guid("e71af559-176d-43ff-821e-760290d62dd6"), 0, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plata facturi", "Plata facturi", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("4817ee60-a647-400f-bf3a-265fe184fe81"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("887e9957-4f9b-4b13-878d-643c5040c19e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("b285dbbc-6696-4dbd-80ff-795c83c13c42"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("b6619b80-4fb7-48a8-8a2e-9383afbdff93"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ID",
                keyValue: new Guid("1261ee94-293e-4a4d-99f6-5e4f42451093"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ID",
                keyValue: new Guid("b7fca1ba-0899-41f0-950a-9bfe2673c931"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ID",
                keyValue: new Guid("e71af559-176d-43ff-821e-760290d62dd6"));

            migrationBuilder.AddColumn<int>(
                name: "BeneficiariesCount",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NGOsCount",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsCount",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteersCount",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BeneficiariesCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NGOsCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteersCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
