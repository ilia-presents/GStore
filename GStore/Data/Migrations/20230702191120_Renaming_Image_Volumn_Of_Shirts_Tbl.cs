using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Renaming_Image_Volumn_Of_Shirts_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Products_Level2Sets_CategoryId",
            //    table: "Products");

            //migrationBuilder.DropIndex(
            //    name: "IX_Products_CategoryId",
            //    table: "Products");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "ImageLink");

            //migrationBuilder.CreateTable(
            //    name: "Level2SetShirt",
            //    columns: table => new
            //    {
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        Level2SetsId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Level2SetShirt", x => new { x.CategoryId, x.Level2SetsId });
            //        table.ForeignKey(
            //            name: "FK_Level2SetShirt_Level2Sets_Level2SetsId",
            //            column: x => x.Level2SetsId,
            //            principalTable: "Level2Sets",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Level2SetShirt_Products_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Products",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Level2SetShirt_Level2SetsId",
            //    table: "Level2SetShirt",
            //    column: "Level2SetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Level2SetShirt");

            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Products",
                newName: "Image");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_CategoryId",
            //    table: "Products",
            //    column: "CategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Level2Sets_CategoryId",
            //    table: "Products",
            //    column: "CategoryId",
            //    principalTable: "Level2Sets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
