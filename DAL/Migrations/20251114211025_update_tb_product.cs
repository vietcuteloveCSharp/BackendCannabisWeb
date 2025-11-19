using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_tb_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                schema: "Products",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BrandPrice",
                schema: "Products",
                table: "Products",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                schema: "Products",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_BRAND_BRANDID",
                schema: "Products",
                table: "Products",
                column: "BrandId",
                principalSchema: "Products",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_BRAND_BRANDID",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandPrice",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductType",
                schema: "Products",
                table: "Products");

            
        }
    }
}
