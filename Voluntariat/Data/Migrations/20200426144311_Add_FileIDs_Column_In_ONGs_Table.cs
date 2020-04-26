using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Add_FileIDs_Column_In_ONGs_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileIDs",
                table: "Ongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileIDs",
                table: "Ongs");
        }
    }
}
