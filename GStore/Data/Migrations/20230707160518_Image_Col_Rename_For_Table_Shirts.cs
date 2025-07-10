using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Image_Col_Rename_For_Table_Shirts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Shirts",
                newName: "ImageLinkOne");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLinkOne",
                table: "Shirts",
                newName: "ImageLink");
        }
    }
}
