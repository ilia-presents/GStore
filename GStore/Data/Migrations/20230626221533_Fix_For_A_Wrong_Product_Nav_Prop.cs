using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Fix_For_A_Wrong_Product_Nav_Prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    //        migrationBuilder.DropForeignKey(
    //name: "FK_Products_Level2Sets_CategoryId",
    //table: "Products");

            migrationBuilder.AddForeignKey(
    name: "FK_Products_CategoryId",
    table: "Products",
    column: "CategoryId",
    principalTable: "Level2Sets",
    principalColumn: "Id",
    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
name: "FK_Products_CategoryId",
table: "Products");
        }
    }
}
