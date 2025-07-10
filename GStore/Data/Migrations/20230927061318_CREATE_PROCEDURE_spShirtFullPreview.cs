using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class CREATE_PROCEDURE_spShirtFullPreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE PROCEDURE [dbo].[spShirtFullShopPreview]
	@ShirtId INT = 0
	,@ShirtColorIsAvalable bit = 1
   	AS
   BEGIN
   	SET NOCOUNT ON;

SELECT shirt.[Id]
      ,shirt.[ImageLinkOne] As ShirtFrontImage
	  ,shirt.[ImageLinkTwo] As ShirtBackImage
      ,shirt.[Name] As ShirtName
      ,shirt.[Description]
      ,shirt.[Price]
	  ,shirt.[IsPromo]
      ,shirt.[Discount]
      ,shirt.[Quantity]
      ,shirt.[IsAvailable] As IsShirtAvailable
      ,shirt.[IsActive] As IsShirtActive
      ,shirtsColorSets.ColorSetId As ColorId
	  ,colorSets.[Name] As ColorName
	  ,colorSets.ColorCode
	  ,shirtsColorSets.ImageLinkOne
	  ,shirtsColorSets.ImageLinkTwo

  FROM [Shirts] As shirt
	INNER JOIN [ShirtsColorSets] As shirtsColorSets
  ON shirt.Id = shirtsColorSets.ProductId
    INNER JOIN [ColorSets] As colorSets
  ON colorSets.Id = shirtsColorSets.ColorSetId
  WHERE (shirt.Id = @ShirtId  
  AND shirtsColorSets.IsAvalable = @ShirtColorIsAvalable)
  END;";
            migrationBuilder.Sql(command);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP PROCEDURE spShirtFullShopPreview;";
            migrationBuilder.Sql(command);
        }
    }
}
