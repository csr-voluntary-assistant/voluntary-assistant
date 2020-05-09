using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Add_Order_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OngID = table.Column<Guid>(nullable: false),
                    DoctorID = table.Column<Guid>(nullable: false),
                    BeneficiaryID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    VolunteerID = table.Column<Guid>(nullable: true),
                    CaretakerID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
