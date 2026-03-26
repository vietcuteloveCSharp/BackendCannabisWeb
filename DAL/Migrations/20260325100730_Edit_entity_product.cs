using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Edit_entity_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirflowRate",
                schema: "Inventory",
                table: "CarbonFilters");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_ProductId",
                schema: "Inventory",
                table: "Seeds",
                newName: "IX_SEED_PRODUCTID");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "Inventory",
                table: "PowerSupplies",
                newName: "PowerSupplyType");

            migrationBuilder.AlterColumn<decimal>(
                name: "Yield",
                schema: "Inventory",
                table: "Seeds",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "THCContent",
                schema: "Inventory",
                table: "Seeds",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "StrainType",
                schema: "Inventory",
                table: "Seeds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                schema: "Inventory",
                table: "Seeds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CBDContent",
                schema: "Inventory",
                table: "Seeds",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "Genetics",
                schema: "Inventory",
                table: "Seeds",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IndoorHeightCm",
                schema: "Inventory",
                table: "Seeds",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationStage",
                schema: "Inventory",
                table: "Nutrients",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DilutionRate",
                schema: "Inventory",
                table: "Nutrients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPhBuffered",
                schema: "Inventory",
                table: "Nutrients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CanvasDensity",
                schema: "Inventory",
                table: "GrowTents",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightCm",
                schema: "Inventory",
                table: "GrowTents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LengthCm",
                schema: "Inventory",
                table: "GrowTents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReflectiveMaterial",
                schema: "Inventory",
                table: "GrowTents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WidthCm",
                schema: "Inventory",
                table: "GrowTents",
                type: "int",
                maxLength: 255,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Efficacy",
                schema: "Inventory",
                table: "GrowLights",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDimmable",
                schema: "Inventory",
                table: "GrowLights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PPF",
                schema: "Inventory",
                table: "GrowLights",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAutoHumidistat",
                schema: "Inventory",
                table: "Dehumidifiers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasContinuousDrainage",
                schema: "Inventory",
                table: "Dehumidifiers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TankCapacityLiters",
                schema: "Inventory",
                table: "Dehumidifiers",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinTemperature",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxTemperature",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Length",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Diameter",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.AddColumn<int>(
                name: "AirflowRateCFM",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CarbonBedThicknessMm",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FlangeSizeInch",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(4,1)",
                precision: 4,
                scale: 1,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_SEEDS_PRICE",
                schema: "Inventory",
                table: "Seeds",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_SEEDS_STRAINTYPE",
                schema: "Inventory",
                table: "Seeds",
                column: "StrainType");

            migrationBuilder.CreateIndex(
                name: "IX_SEEDS_THC",
                schema: "Inventory",
                table: "Seeds",
                column: "THCContent");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrient_Organic",
                schema: "Inventory",
                table: "Nutrients",
                column: "IsOrganic");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrient_Price",
                schema: "Inventory",
                table: "Nutrients",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrient_Stage",
                schema: "Inventory",
                table: "Nutrients",
                column: "ApplicationStage");

            migrationBuilder.CreateIndex(
                name: "IX_GrowTents_Height",
                schema: "Inventory",
                table: "GrowTents",
                column: "HeightCm");

            migrationBuilder.CreateIndex(
                name: "IX_GrowTents_Price",
                schema: "Inventory",
                table: "GrowTents",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_GrowTents_Width",
                schema: "Inventory",
                table: "GrowTents",
                column: "WidthCm");

            migrationBuilder.CreateIndex(
                name: "IX_GrowLights_Price",
                schema: "Inventory",
                table: "GrowLights",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_GrowLights_Wattage",
                schema: "Inventory",
                table: "GrowLights",
                column: "Wattage");

            migrationBuilder.CreateIndex(
                name: "IX_Dehumidifier_Capacity",
                schema: "Inventory",
                table: "Dehumidifiers",
                column: "DehumidificationCapacity");

            migrationBuilder.CreateIndex(
                name: "IX_Dehumidifier_Power",
                schema: "Inventory",
                table: "Dehumidifiers",
                column: "PowerConsumption");

            migrationBuilder.CreateIndex(
                name: "IX_CarbonFilter_Airflow",
                schema: "Inventory",
                table: "CarbonFilters",
                column: "AirflowRateCFM");

            migrationBuilder.CreateIndex(
                name: "IX_CarbonFilter_FlangeSize",
                schema: "Inventory",
                table: "CarbonFilters",
                column: "FlangeSizeInch");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SEEDS_PRICE",
                schema: "Inventory",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_SEEDS_STRAINTYPE",
                schema: "Inventory",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_SEEDS_THC",
                schema: "Inventory",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Nutrient_Organic",
                schema: "Inventory",
                table: "Nutrients");

            migrationBuilder.DropIndex(
                name: "IX_Nutrient_Price",
                schema: "Inventory",
                table: "Nutrients");

            migrationBuilder.DropIndex(
                name: "IX_Nutrient_Stage",
                schema: "Inventory",
                table: "Nutrients");

            migrationBuilder.DropIndex(
                name: "IX_GrowTents_Height",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropIndex(
                name: "IX_GrowTents_Price",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropIndex(
                name: "IX_GrowTents_Width",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropIndex(
                name: "IX_GrowLights_Price",
                schema: "Inventory",
                table: "GrowLights");

            migrationBuilder.DropIndex(
                name: "IX_GrowLights_Wattage",
                schema: "Inventory",
                table: "GrowLights");

            migrationBuilder.DropIndex(
                name: "IX_Dehumidifier_Capacity",
                schema: "Inventory",
                table: "Dehumidifiers");

            migrationBuilder.DropIndex(
                name: "IX_Dehumidifier_Power",
                schema: "Inventory",
                table: "Dehumidifiers");

            migrationBuilder.DropIndex(
                name: "IX_CarbonFilter_Airflow",
                schema: "Inventory",
                table: "CarbonFilters");

            migrationBuilder.DropIndex(
                name: "IX_CarbonFilter_FlangeSize",
                schema: "Inventory",
                table: "CarbonFilters");

            migrationBuilder.DropColumn(
                name: "Genetics",
                schema: "Inventory",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "IndoorHeightCm",
                schema: "Inventory",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "ApplicationStage",
                schema: "Inventory",
                table: "Nutrients");

            migrationBuilder.DropColumn(
                name: "DilutionRate",
                schema: "Inventory",
                table: "Nutrients");

            migrationBuilder.DropColumn(
                name: "IsPhBuffered",
                schema: "Inventory",
                table: "Nutrients");

            migrationBuilder.DropColumn(
                name: "CanvasDensity",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropColumn(
                name: "HeightCm",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropColumn(
                name: "LengthCm",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropColumn(
                name: "ReflectiveMaterial",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropColumn(
                name: "WidthCm",
                schema: "Inventory",
                table: "GrowTents");

            migrationBuilder.DropColumn(
                name: "Efficacy",
                schema: "Inventory",
                table: "GrowLights");

            migrationBuilder.DropColumn(
                name: "IsDimmable",
                schema: "Inventory",
                table: "GrowLights");

            migrationBuilder.DropColumn(
                name: "PPF",
                schema: "Inventory",
                table: "GrowLights");

            migrationBuilder.DropColumn(
                name: "HasAutoHumidistat",
                schema: "Inventory",
                table: "Dehumidifiers");

            migrationBuilder.DropColumn(
                name: "HasContinuousDrainage",
                schema: "Inventory",
                table: "Dehumidifiers");

            migrationBuilder.DropColumn(
                name: "TankCapacityLiters",
                schema: "Inventory",
                table: "Dehumidifiers");

            migrationBuilder.DropColumn(
                name: "AirflowRateCFM",
                schema: "Inventory",
                table: "CarbonFilters");

            migrationBuilder.DropColumn(
                name: "CarbonBedThicknessMm",
                schema: "Inventory",
                table: "CarbonFilters");

            migrationBuilder.DropColumn(
                name: "FlangeSizeInch",
                schema: "Inventory",
                table: "CarbonFilters");

            migrationBuilder.RenameIndex(
                name: "IX_SEED_PRODUCTID",
                schema: "Inventory",
                table: "Seeds",
                newName: "IX_Seeds_ProductId");

            migrationBuilder.RenameColumn(
                name: "PowerSupplyType",
                schema: "Inventory",
                table: "PowerSupplies",
                newName: "Type");

            migrationBuilder.AlterColumn<decimal>(
                name: "Yield",
                schema: "Inventory",
                table: "Seeds",
                type: "decimal(5,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "THCContent",
                schema: "Inventory",
                table: "Seeds",
                type: "varchar(30)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "StrainType",
                schema: "Inventory",
                table: "Seeds",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                schema: "Inventory",
                table: "Seeds",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CBDContent",
                schema: "Inventory",
                table: "Seeds",
                type: "varchar(30)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinTemperature",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxTemperature",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Length",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Diameter",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "AirflowRate",
                schema: "Inventory",
                table: "CarbonFilters",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
