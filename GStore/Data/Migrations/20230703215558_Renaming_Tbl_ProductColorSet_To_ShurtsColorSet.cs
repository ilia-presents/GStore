using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Renaming_Tbl_ProductColorSet_To_ShurtsColorSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsColorSets_ColorSets_ColorSetId",
                table: "ProductsColorSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsColorSets_Shirts_ProductId",
                table: "ProductsColorSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsColorSets",
                table: "ProductsColorSets");

            migrationBuilder.RenameTable(
                name: "ProductsColorSets",
                newName: "ShirtsColorSets");

            //migrationBuilder.RenameIndex(
            //    name: "IX_ProductsColorSets_ColorSetId",
            //    table: "ShirtsColorSets",
            //    newName: "IX_ShirtsColorSets_ColorSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShirtsColorSets",
                table: "ShirtsColorSets",
                columns: new[] { "ProductId", "ColorSetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShirtsColorSets_ColorSets_ColorSetId",
                table: "ShirtsColorSets",
                column: "ColorSetId",
                principalTable: "ColorSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShirtsColorSets_Shirts_ProductId",
                table: "ShirtsColorSets",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShirtsColorSets_ColorSets_ColorSetId",
                table: "ShirtsColorSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ShirtsColorSets_Shirts_ProductId",
                table: "ShirtsColorSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShirtsColorSets",
                table: "ShirtsColorSets");

            migrationBuilder.RenameTable(
                name: "ShirtsColorSets",
                newName: "ProductsColorSets");

            migrationBuilder.RenameIndex(
                name: "IX_ShirtsColorSets_ColorSetId",
                table: "ProductsColorSets",
                newName: "IX_ProductsColorSets_ColorSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsColorSets",
                table: "ProductsColorSets",
                columns: new[] { "ProductId", "ColorSetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsColorSets_ColorSets_ColorSetId",
                table: "ProductsColorSets",
                column: "ColorSetId",
                principalTable: "ColorSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsColorSets_Shirts_ProductId",
                table: "ProductsColorSets",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
