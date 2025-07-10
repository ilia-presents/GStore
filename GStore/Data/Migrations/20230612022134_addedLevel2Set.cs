using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class addedLevel2Set : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Level2Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(42)", maxLength: 42, nullable: false),
                    Level1SetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level2Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Level2Sets_Level1Sets_Level1SetId",
                        column: x => x.Level1SetId,
                        principalTable: "Level1Sets",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Level2Sets_Level1SetId",
            //    table: "Level2Sets",
            //    column: "Level1SetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Level2Sets");
        }
    }
}
