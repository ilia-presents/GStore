using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Extended_Colors_Code_Varchar_To_82 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "ColorSets",
                type: "varchar(82)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "ColorSets",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(82)");
        }
    }
}
