using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Add_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ongs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ada98e1-4054-4d9a-a591-0dfd20d4cea3", "471befb4-f2ec-434f-a195-b3963a502715", "Admin", "Admin" },
                    { "731570ea-7c31-462f-bc9c-bcaafba892a1", "471befb4-f2ec-434f-a195-b3963a502717", "Volunteer", "Volunteer" },
                    { "c449f071-e14e-469e-affe-1c8b2269cc3f", "471befb4-f2ec-434f-a195-b3963a502718", "Doctor", "Doctor" },
                    { "b9aedc08-76f8-4017-9156-27180e377dca", "471befb4-f2ec-434f-a195-b3963a502719", "Beneficiary", "Beneficiary" },
                    { "bd0d075f-663a-445f-8902-b555a09b1d2d", "471befb4-f2ec-434f-a195-b3963a502716", "Guest", "Guest" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "731570ea-7c31-462f-bc9c-bcaafba892a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ada98e1-4054-4d9a-a591-0dfd20d4cea3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9aedc08-76f8-4017-9156-27180e377dca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd0d075f-663a-445f-8902-b555a09b1d2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c449f071-e14e-469e-affe-1c8b2269cc3f");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ongs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
