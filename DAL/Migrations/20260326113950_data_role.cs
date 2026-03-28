using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class data_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6629));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6642));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6643));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6645));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6646));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6830));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6832));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6835));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6835));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6915));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6916));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6917));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6918));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6919));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6887));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6889));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6890));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6891));

            migrationBuilder.InsertData(
                schema: "Users",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(7013), null, "Quản trị viên hệ thống - Toàn quyền cấu hình", false, "Admin", null },
                    { 2, new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(7016), null, "Người dùng", false, "User", null },
                    { 3, new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(7017), null, "Nhân viên - Nhập liệu và vận hành kho", false, "Employee", null }
                });

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6859));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6864));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 39, 50, 331, DateTimeKind.Utc).AddTicks(6865));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Users",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Users",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Users",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8733));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8742));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8744));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8745));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "ChipModels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8746));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8935));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8938));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8940));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8941));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "CoolingSystems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8942));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9033));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9036));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9037));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "NutrientTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9038));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9003));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9006));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9007));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "PowerSupplies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(9008));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8971));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8977));

            migrationBuilder.UpdateData(
                schema: "Inventory",
                table: "Spectrums",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 11, 31, 45, 482, DateTimeKind.Utc).AddTicks(8978));
        }
    }
}
