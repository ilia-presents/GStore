using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class CREATE_PROCEDURE_spShirtPreviewWithCatName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE OR Alter PROCEDURE spShirtPrevWithCatName
                        @RowsToSkip INT = 0
                        ,@RowsToTake INT = 22
						,@ShirtCategoryId INT = NULL
						,@ShirtIsPromo bit = 0
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
                    , Shirt.IsActive
                    , Shirt.IsAvailable
					, l2Category.[Name] As [CategoryName]
                    FROM Shirts AS Shirt 
                    inner join vwColorShirtAvailability as vwColorShAv
                    ON Shirt.Id = vwColorShAv.ProductId
					inner join Level2Sets as l2Category
					ON Shirt.CategoryId = l2Category.Id
					WHERE ((@ShirtCategoryId IS NULL) OR (Shirt.CategoryId = @ShirtCategoryId)) AND
					 (Shirt.IsPromo = @ShirtIsPromo)
					 ORDER BY Shirt.Id DESC
                    OFFSET @RowsToSkip ROWS
                    FETCH NEXT @RowsToTake ROWS ONLY;
                    END;";
            migrationBuilder.Sql(command);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP PROCEDURE spShirtPrevWithCatName;";
            migrationBuilder.Sql(command);

        }
    }
}
