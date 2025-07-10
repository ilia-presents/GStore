using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Rename_Column_ForShirts_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLinktwo",
                table: "Shirts",
                newName: "ImageLinkTwo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLinkTwo",
                table: "Shirts",
                newName: "ImageLinktwo");
        }
    }
}
