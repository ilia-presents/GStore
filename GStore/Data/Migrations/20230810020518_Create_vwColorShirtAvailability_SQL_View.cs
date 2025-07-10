using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GStore.Data.Migrations
{
    public partial class Create_vwColorShirtAvailability_SQL_View : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE VIEW vwColorShirtAvailability AS 
            SELECT  dbo.ShirtsColorSets.ProductId, dbo.ColorSets.ColorCode
            FROM    dbo.ShirtsColorSets INNER JOIN
            dbo.ColorSets ON dbo.ShirtsColorSets.ColorSetId = dbo.ColorSets.Id
            WHERE  (dbo.ShirtsColorSets.IsAvalable = 1)
";
        migrationBuilder.Sql(command);
        }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        var command = @"DROP VIEW vwColorShirtAvailability;";
        migrationBuilder.Sql(command);
    }
}
}
