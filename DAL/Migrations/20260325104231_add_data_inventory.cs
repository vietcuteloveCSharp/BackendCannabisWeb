using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_data_inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "ChipModels",
                columns: new[] { "ChipModelId", "CreatedAt", "DeletedAt", "Description", "Efficiency", "Generation", "IsDeleted", "Manufacturer", "ModelChip", "ModelName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2619), null, "Top tier for horticulture", 3.10m, null, false, "Samsung", "LM301H", "Evo", null },
                    { 2, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2628), null, "Hyper Red 660nm", 4.00m, null, false, "Osram", "GH CSSRM4.24", "Oslon Square", null },
                    { 3, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2630), null, "Cost-effective solution", 2.80m, null, false, "Cree", "JK2835", "J Series", null },
                    { 4, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2631), null, "High power COB", 2.60m, null, false, "Bridgelux", "BXEB-L0340", "Vero 29", null },
                    { 5, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2668), null, "Full spectrum natural light", 2.75m, null, false, "Seoul", "MJT-3030", "SunLike", null }
                });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "CoolingSystems",
                columns: new[] { "CoolingSystemId", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2820), null, "Aluminium Heatsink", false, "Fan", null },
                    { 2, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2824), null, "Dual Ball Bearing Fan", false, "WaterCooling", null },
                    { 3, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2825), null, "Water cooling block", false, "AirConditioning", null },
                    { 4, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2826), null, "Smart PWM Fan", false, "Fan", null },
                    { 5, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2827), null, "Graphene Coating", false, "AirConditioning", null }
                });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "NutrientTypes",
                columns: new[] { "NutrientTypeId", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "NutrientName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2922), null, "Essential N-P-K foundation for all plant stages.", false, "Base Nutrients", null },
                    { 2, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2928), null, "Enhances root development and nutrient uptake efficiency.", false, "Root Stimulators", null },
                    { 3, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2929), null, "High Phosphorus and Potassium for massive flower production.", false, "Bloom Boosters", null },
                    { 4, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2930), null, "Prevents common deficiencies in Coco Coir or RO water.", false, "Cal-Mag Supplements", null },
                    { 5, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2931), null, "Solutions to maintain optimal pH levels (5.5 - 6.5).", false, "pH Adjusters", null }
                });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "PowerSupplies",
                columns: new[] { "PowerSupplyId", "CreatedAt", "DeletedAt", "IsDeleted", "PowerSupplyType", "UpdatedAt", "Voltage" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2889), null, false, "Internal", null, 48 },
                    { 2, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2891), null, false, "Driverless", null, 24 },
                    { 3, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2892), null, false, "External", null, 36 },
                    { 4, new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2893), null, false, "Removable", null, 54 }
                });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "Spectrums",
                columns: new[] { "SpectrumId", "ColorHexCode", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "SpectrumChartUrl", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "#FFFFFF", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2854), null, "Balanced growth", false, "https://cdn.example.com/s1.png", "FullSpectrum", null },
                    { 2, "#FF5733", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2862), null, "Flowering stage boost", false, "https://cdn.example.com/s2.png", "Flowering", null },
                    { 3, "#33FF57", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2863), null, "Vegetative growth", false, "https://cdn.example.com/s3.png", "Vegetative", null },
                    { 4, "#4B0082", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2864), null, "Terpene production", false, "https://cdn.example.com/s4.png", "DualSpectrum", null },
                    { 5, "#4B8875", new DateTime(2026, 3, 25, 10, 42, 30, 898, DateTimeKind.Utc).AddTicks(2865), null, "Terpene production", false, "https://cdn.example.com/s4.png", "Customized", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "ChipModelId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "CoolingSystemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "NutrientTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "PowerSupplyId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "SpectrumId",
                keyValue: 3);

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
        }
    }
}
