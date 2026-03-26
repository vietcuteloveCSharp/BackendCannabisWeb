using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_column_spectrum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "CRI",
                schema: "Inventory",
                table: "Spectrums",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorTemperatureK",
                schema: "Inventory",
                table: "Spectrums",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1798));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1806));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1807));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1809));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1810));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1995));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(1999));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2001));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2002));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2145));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2147));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2111));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2115));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 1,
                columns: new[] { "CRI", "ColorHexCode", "ColorTemperatureK", "CreatedAt", "Description", "SpectrumChartUrl" },
                values: new object[] { 90, "#FDF4E3", 3500, new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2084), null, "/images/spectrums/full-spectrum.jpg" });

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 2,
                columns: new[] { "CRI", "ColorHexCode", "ColorTemperatureK", "CreatedAt", "Description", "SpectrumChartUrl", "Type" },
                values: new object[] { 85, "#1E90FF", 6500, new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2089), null, "/images/spectrums/veg-spectrum.jpg", "Vegetative" });

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 3,
                columns: new[] { "CRI", "ColorHexCode", "ColorTemperatureK", "CreatedAt", "Description", "SpectrumChartUrl", "Type" },
                values: new object[] { 88, "#FF4500", 2700, new DateTime(2026, 3, 26, 8, 41, 10, 23, DateTimeKind.Utc).AddTicks(2091), null, "/images/spectrums/bloom-spectrum.jpg", "Flowering" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRI",
                schema: "Inventory",
                table: "Spectrums");

            migrationBuilder.DropColumn(
                name: "ColorTemperatureK",
                schema: "Inventory",
                table: "Spectrums");

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2619));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2628));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2631));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2668));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2827));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2928));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2929));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2930));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2931));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2889));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2891));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2892));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2893));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 1,
                columns: new[] { "ColorHexCode", "CreatedAt", "Description", "SpectrumChartUrl" },
                values: new object[] { "#FFFFFF", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2854), "Balanced growth", "https://cdn.example.com/s1.png" });

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 2,
                columns: new[] { "ColorHexCode", "CreatedAt", "Description", "SpectrumChartUrl", "Type" },
                values: new object[] { "#FF5733", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2862), "Flowering stage boost", "https://cdn.example.com/s2.png", "Flowering" });

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 3,
                columns: new[] { "ColorHexCode", "CreatedAt", "Description", "SpectrumChartUrl", "Type" },
                values: new object[] { "#33FF57", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2863), "Vegetative growth", "https://cdn.example.com/s3.png", "Vegetative" });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "Spectrums",
                columns: new[] { "SpectrumId", "ColorHexCode", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "SpectrumChartUrl", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "#4B0082", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2864), null, "Terpene production", false, "https://cdn.example.com/s4.png", "DualSpectrum", null },
                    { 5, "#4B8875", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2865), null, "Terpene production", false, "https://cdn.example.com/s4.png", "Customized", null }
                });
        }
    }
}
