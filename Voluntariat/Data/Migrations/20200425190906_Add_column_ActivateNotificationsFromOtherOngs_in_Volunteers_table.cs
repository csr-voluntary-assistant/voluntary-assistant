using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Add_column_ActivateNotificationsFromOtherOngs_in_Volunteers_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActivateNotificationsFromOtherOngs",
                table: "Volunteers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivateNotificationsFromOtherOngs",
                table: "Volunteers");
        }
    }
}
