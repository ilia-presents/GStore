using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Added_ProductExtraImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Level2SetShirt");

            migrationBuilder.CreateTable(
                name: "ProductExtraImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageLinkOne = table.Column<string>(type: "nvarchar(190)", maxLength: 190, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExtraImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductExtraImages_Shirts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Shirts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Shirts_CategoryId",
            //    table: "Shirts",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductExtraImages_ProductId",
            //    table: "ProductExtraImages",
            //    column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shirts_Level2Sets_CategoryId",
                table: "Shirts",
                column: "CategoryId",
                principalTable: "Level2Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shirts_Level2Sets_CategoryId",
                table: "Shirts");

            migrationBuilder.DropTable(
                name: "ProductExtraImages");

            //migrationBuilder.DropIndex(
            //    name: "IX_Shirts_CategoryId",
            //    table: "Shirts");

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
            //            name: "FK_Level2SetShirt_Shirts_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Shirts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Level2SetShirt_Level2SetsId",
            //    table: "Level2SetShirt",
            //    column: "Level2SetsId");
        }
    }
}
