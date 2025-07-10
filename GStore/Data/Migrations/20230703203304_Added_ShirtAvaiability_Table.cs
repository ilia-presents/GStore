using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Added_ShirtAvaiability_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShirtAvailabilitys",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizeSetId = table.Column<int>(type: "int", nullable: false),
                    ColorSetId = table.Column<int>(type: "int", nullable: false),
                    IsAvalable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShirtAvailabilitys", x => new { x.ProductId, x.SizeSetId, x.ColorSetId });
                    table.ForeignKey(
                        name: "FK_ShirtAvailabilitys_ColorSets_ColorSetId",
                        column: x => x.ColorSetId,
                        principalTable: "ColorSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShirtAvailabilitys_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShirtAvailabilitys_SizeSets_SizeSetId",
                        column: x => x.SizeSetId,
                        principalTable: "SizeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShirtAvailabilitys_ColorSetId",
            //    table: "ShirtAvailabilitys",
            //    column: "ColorSetId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShirtAvailabilitys_SizeSetId",
            //    table: "ShirtAvailabilitys",
            //    column: "SizeSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShirtAvailabilitys");
        }
    }
}
