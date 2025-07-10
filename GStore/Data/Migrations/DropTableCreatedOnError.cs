using Microsoft.EntityFrameworkCore.Migrations;

namespace GStore.Data.Migrations
{
    public class DropTableCreatedOnError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Level2SetShirt");
        }
    }
}
