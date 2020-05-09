using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Rename_Table_Doctor_To_Beneficiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c449f071-e14e-469e-affe-1c8b2269cc3f");

            migrationBuilder.DropColumn(
                name: "CaretakerID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OngID = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.AddColumn<Guid>(
                name: "CaretakerID",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorID",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OngID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c449f071-e14e-469e-affe-1c8b2269cc3f", "471befb4-f2ec-434f-a195-b3963a502718", "Doctor", "Doctor" });
        }
    }
}
