using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class addedProductColorsAndSizesRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsColorSets",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorSetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsColorSets", x => new { x.ProductId, x.ColorSetId });
                    table.ForeignKey(
                        name: "FK_ProductsColorSets_ColorSets_ColorSetId",
                        column: x => x.ColorSetId,
                        principalTable: "ColorSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsColorSets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSizeSets",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizeSetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSizeSets", x => new { x.ProductId, x.SizeSetId });
                    table.ForeignKey(
                        name: "FK_ProductsSizeSets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSizeSets_SizeSets_SizeSetId",
                        column: x => x.SizeSetId,
                        principalTable: "SizeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductsColorSets_ColorSetId",
            //    table: "ProductsColorSets",
            //    column: "ColorSetId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductsSizeSets_SizeSetId",
            //    table: "ProductsSizeSets",
            //    column: "SizeSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsColorSets");

            migrationBuilder.DropTable(
                name: "ProductsSizeSets");
        }
    }
}
