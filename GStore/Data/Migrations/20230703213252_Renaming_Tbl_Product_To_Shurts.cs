using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Renaming_Tbl_Product_To_Shurts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Level2SetShirt_Products_CategoryId",
            //    table: "Level2SetShirt");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsColorSets_Products_ProductId",
                table: "ProductsColorSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSizeSets_Products_ProductId",
                table: "ProductsSizeSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ShirtAvailabilitys_Products_ProductId",
                table: "ShirtAvailabilitys");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Shirts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shirts",
                table: "Shirts",
                column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Level2SetShirt_Shirts_CategoryId",
            //    table: "Level2SetShirt",
            //    column: "CategoryId",
            //    principalTable: "Shirts",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsColorSets_Shirts_ProductId",
                table: "ProductsColorSets",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSizeSets_Shirts_ProductId",
                table: "ProductsSizeSets",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShirtAvailabilitys_Shirts_ProductId",
                table: "ShirtAvailabilitys",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Shirts_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Level2SetShirt_Shirts_CategoryId",
                table: "Level2SetShirt");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsColorSets_Shirts_ProductId",
                table: "ProductsColorSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSizeSets_Shirts_ProductId",
                table: "ProductsSizeSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ShirtAvailabilitys_Shirts_ProductId",
                table: "ShirtAvailabilitys");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Shirts_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shirts",
                table: "Shirts");

            migrationBuilder.RenameTable(
                name: "Shirts",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Level2SetShirt_Products_CategoryId",
                table: "Level2SetShirt",
                column: "CategoryId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsColorSets_Products_ProductId",
                table: "ProductsColorSets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSizeSets_Products_ProductId",
                table: "ProductsSizeSets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShirtAvailabilitys_Products_ProductId",
                table: "ShirtAvailabilitys",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
