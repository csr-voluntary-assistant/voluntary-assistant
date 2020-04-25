using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voluntariat.Data.Migrations
{
    public partial class Add_Table_BeneficiaryCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeneficiaryCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiaryCategories", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "BeneficiaryCategories",
                columns: new[] { "ID", "Description" },
                values: new object[,]
                {
                    { new Guid("4817ee60-a647-400f-bf3a-265fe184fe81"), "Seniori 65+" },
                    { new Guid("b6619b80-4fb7-48a8-8a2e-9383afbdff93"), "Persoane cu dizabilitați" },
                    { new Guid("887e9957-4f9b-4b13-878d-643c5040c19e"), "Persoane autoizolate" },
                    { new Guid("b285dbbc-6696-4dbd-80ff-795c83c13c42"), "Cazuri sociale" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiaryCategories");
        }
    }
}
