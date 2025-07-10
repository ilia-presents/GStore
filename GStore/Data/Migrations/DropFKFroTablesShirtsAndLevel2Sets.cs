using Microsoft.EntityFrameworkCore.Migrations;

namespace GStore.Data.Migrations
{
    public partial class DropFKForTablesShirtsAndLevel2Sets
        : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_Products_CategoryId",
    table: "Shirts");
        }
    }
}
