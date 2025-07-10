using Microsoft.EntityFrameworkCore.Migrations;

namespace GStore.Data.Migrations
{
    public class Rename_ProductId_col_ToShirtId_On_ShirtsColorSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "ProductId",
            table: "ShirtColorSet",
            newName: "ShirtId",
            schema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "ShirtId",
            table: "ShirtColorSet",
            newName: "ProductId",
            schema: "dbo");
        }

    }
}
