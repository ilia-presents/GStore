using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Added_IsAvalable_Columns_For_ProductSizeSet_ProductColorSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvalable",
                table: "ProductsSizeSets",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvalable",
                table: "ProductsColorSets",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvalable",
                table: "ProductsSizeSets");

            migrationBuilder.DropColumn(
                name: "IsAvalable",
                table: "ProductsColorSets");
        }
    }
}
