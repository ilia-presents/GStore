using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Renaming_Tbl_ProductSizeSet_To_ShurtsSizeSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSizeSets_Shirts_ProductId",
                table: "ProductsSizeSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSizeSets_SizeSets_SizeSetId",
                table: "ProductsSizeSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSizeSets",
                table: "ProductsSizeSets");

            migrationBuilder.RenameTable(
                name: "ProductsSizeSets",
                newName: "ShirtsSizeSets");

            //migrationBuilder.RenameIndex(
            //    name: "IX_ProductsSizeSets_SizeSetId",
            //    table: "ShirtsSizeSets",
            //    newName: "IX_ShirtsSizeSets_SizeSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShirtsSizeSets",
                table: "ShirtsSizeSets",
                columns: new[] { "ProductId", "SizeSetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShirtsSizeSets_Shirts_ProductId",
                table: "ShirtsSizeSets",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShirtsSizeSets_SizeSets_SizeSetId",
                table: "ShirtsSizeSets",
                column: "SizeSetId",
                principalTable: "SizeSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShirtsSizeSets_Shirts_ProductId",
                table: "ShirtsSizeSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ShirtsSizeSets_SizeSets_SizeSetId",
                table: "ShirtsSizeSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShirtsSizeSets",
                table: "ShirtsSizeSets");

            migrationBuilder.RenameTable(
                name: "ShirtsSizeSets",
                newName: "ProductsSizeSets");

            migrationBuilder.RenameIndex(
                name: "IX_ShirtsSizeSets_SizeSetId",
                table: "ProductsSizeSets",
                newName: "IX_ProductsSizeSets_SizeSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSizeSets",
                table: "ProductsSizeSets",
                columns: new[] { "ProductId", "SizeSetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSizeSets_Shirts_ProductId",
                table: "ProductsSizeSets",
                column: "ProductId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSizeSets_SizeSets_SizeSetId",
                table: "ProductsSizeSets",
                column: "SizeSetId",
                principalTable: "SizeSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
