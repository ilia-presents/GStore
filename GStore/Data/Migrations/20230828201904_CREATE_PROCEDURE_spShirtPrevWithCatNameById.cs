using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class CREATE_PROCEDURE_spShirtPrevWithCatNameById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE OR Alter PROCEDURE spShirtPrevWithCatNameById
                        @ShirtId INT = 0
                    	AS
                    BEGIN
                    	SET NOCOUNT ON;
                    SELECT 
                    Shirt.Id
                    , Shirt.[Name]
                    , Shirt.ImageLinkOne
                    , Shirt.ImageLinkTwo
                    , Shirt.Price
                    , Shirt.CategoryId
                    , vwColorShAv.ColorCode
                    , Shirt.IsPromo
                    , Shirt.Discount
					, l2Category.[Name] As [CategoryName]
                    FROM Shirts AS Shirt 
                    inner join vwColorShirtAvailability as vwColorShAv
                    ON Shirt.Id = vwColorShAv.ProductId
					inner join Level2Sets as l2Category
					ON Shirt.CategoryId = l2Category.Id
					WHERE (Shirt.Id = @ShirtId)
                    END;";
            migrationBuilder.Sql(command);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP PROCEDURE spShirtPrevWithCatNameById;";
            migrationBuilder.Sql(command);
        }
    }
}
