using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class CREATE_PROCEDURE_spShirtPreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE PROCEDURE spShirtPreview
                        @RowsToSkip INT = 0
                        ,@RowsToTake INT = 22
						,@ShirtCategoryId INT = NULL
						,@ShirtIsPromo bit = 0
                        ,@ShirtIsAcive bit = 1
						,@ShirtPriceGreater decimal = NULL
						,@ShirtPriceLess decimal = NULL
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
                    FROM Shirts AS Shirt 
                    inner join vwColorShirtAvailability as vwColorShAv
                    ON Shirt.Id = vwColorShAv.ProductId
					WHERE ((@ShirtCategoryId IS NULL) OR (Shirt.CategoryId = @ShirtCategoryId)) AND
					 (Shirt.IsPromo = @ShirtIsPromo) AND
                    (Shirt.IsActive = @ShirtIsAcive) AND
					  ((@ShirtPriceGreater IS NULL) OR (Shirt.Price > @ShirtPriceGreater)) AND 
					  ((@ShirtPriceLess IS NULL) OR (Shirt.Price < @ShirtPriceLess))
                    ORDER BY Shirt.Id DESC
                    OFFSET @RowsToSkip ROWS
                    FETCH NEXT @RowsToTake ROWS ONLY;
                    END;";
            migrationBuilder.Sql(command);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP PROCEDURE spShirtPreview;";
            migrationBuilder.Sql(command);
        }
    }
}
