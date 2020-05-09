using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Drop_Foreign_Keys_And_Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Ongs_OngID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_Ongs_OngID",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_OngID",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_OngID",
                table: "Doctors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_OngID",
                table: "Volunteers",
                column: "OngID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_OngID",
                table: "Doctors",
                column: "OngID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Ongs_OngID",
                table: "Doctors",
                column: "OngID",
                principalTable: "Ongs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Ongs_OngID",
                table: "Volunteers",
                column: "OngID",
                principalTable: "Ongs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
