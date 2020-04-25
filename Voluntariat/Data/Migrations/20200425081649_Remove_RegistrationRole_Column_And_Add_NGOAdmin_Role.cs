using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Remove_RegistrationRole_Column_And_Add_NGOAdmin_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationRole",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "691190d2-05e1-4dcc-a8bc-a378dc518e29", "614b9ffd-8694-4610-bbf0-8c4fe82b6799", "NGOAdmin", "NGOAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "691190d2-05e1-4dcc-a8bc-a378dc518e29");

            migrationBuilder.AddColumn<int>(
                name: "RegistrationRole",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
