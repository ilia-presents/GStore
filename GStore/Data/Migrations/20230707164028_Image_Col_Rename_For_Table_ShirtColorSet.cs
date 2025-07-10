using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Image_Col_Rename_For_Table_ShirtColorSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "ShirtsColorSets");

            migrationBuilder.AddColumn<string>(
                name: "ImageLinkOne",
                table: "ShirtsColorSets",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLinkOne",
                table: "ShirtsColorSets");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "ShirtsColorSets",
                type: "varchar(200)",
                nullable: true);
        }
    }
}
