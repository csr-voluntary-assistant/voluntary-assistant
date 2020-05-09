using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Change_ActionLimit_type_to_decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ActionLimit",
                table: "AspNetUsers",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ActionLimit",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");
        }
    }
}
