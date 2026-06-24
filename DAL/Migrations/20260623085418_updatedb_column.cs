using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedb_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_SELLER_SELLERID",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SellerId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductVariantId",
                schema: "Inventory",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Stock",
                schema: "Products",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingFee",
                schema: "Ship",
                table: "Shipments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Payments",
                table: "Payments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                schema: "Orders",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinOrderAmount",
                schema: "Promotions",
                table: "Coupons",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountAmount",
                schema: "Promotions",
                table: "Coupons",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "Cart",
                table: "Carts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                schema: "Users",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SKU",
                schema: "Products",
                table: "ProductVariants",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffId",
                schema: "Orders",
                table: "Orders",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductVariantId",
                schema: "Inventory",
                table: "Inventories",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_WarehouseId",
                schema: "Inventory",
                table: "Inventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                schema: "Promotions",
                table: "Coupons",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Warehouses_WarehouseId",
                schema: "Inventory",
                table: "Inventories",
                column: "WarehouseId",
                principalSchema: "Inventory",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_SELLER_StaffId",
                schema: "Orders",
                table: "Orders",
                column: "StaffId",
                principalSchema: "Users",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Warehouses_WarehouseId",
                schema: "Inventory",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_SELLER_StaffId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Warehouses",
                schema: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_SKU",
                schema: "Products",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StaffId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductVariantId",
                schema: "Inventory",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_WarehouseId",
                schema: "Inventory",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_Code",
                schema: "Promotions",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "StaffId",
                schema: "Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Inventory",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Ward",
                schema: "Users",
                table: "Addresses");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingFee",
                schema: "Ship",
                table: "Shipments",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                schema: "Products",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Payments",
                table: "Payments",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                schema: "Orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinOrderAmount",
                schema: "Promotions",
                table: "Coupons",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountAmount",
                schema: "Promotions",
                table: "Coupons",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "Cart",
                table: "Carts",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SellerId",
                schema: "Orders",
                table: "Orders",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductVariantId",
                schema: "Inventory",
                table: "Inventories",
                column: "ProductVariantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_SELLER_SELLERID",
                schema: "Orders",
                table: "Orders",
                column: "SellerId",
                principalSchema: "Users",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
