using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Edit_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Classifications_ClassificationName",
                schema: "Products",
                table: "Classifications");

            migrationBuilder.DropColumn(
                name: "ClassificationName",
                schema: "Products",
                table: "Classifications");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Products",
                table: "Classifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Products",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "ColorHexCode",
                schema: "Inventory",
                table: "Spectrums",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpectrumChartUrl",
                schema: "Inventory",
                table: "Spectrums",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Products",
                table: "Classifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                schema: "Inventory",
                table: "ChipModels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                schema: "Products",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHexCode",
                schema: "Inventory",
                table: "Spectrums");

            migrationBuilder.DropColumn(
                name: "SpectrumChartUrl",
                schema: "Inventory",
                table: "Spectrums");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Products",
                table: "Classifications");

            migrationBuilder.DropColumn(
                name: "ModelName",
                schema: "Inventory",
                table: "ChipModels");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                schema: "Products",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "ClassificationName",
                schema: "Products",
                table: "Classifications",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Products",
                table: "Classifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Products",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "UX_Classifications_ClassificationName",
                schema: "Products",
                table: "Classifications",
                column: "ClassificationName",
                unique: true);
        }
    }
}
