using DAL.Entities.Inventory;

namespace DAL.Configurations.SchemaInventory
{
	public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
	{
		public void Configure(EntityTypeBuilder<Inventory> builder)
		{
			builder.ToTable("Inventories", "Inventory");

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Quantity)
				.IsRequired()
				.HasDefaultValue(0);

			// FK ProductVariant
			builder.HasOne(i => i.ProductVariant)
				.WithMany(v => v.Inventories)
				.HasForeignKey(i => i.ProductVariantId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			//FK Warehouse
			builder.HasOne(i => i.Warehouse)
			.WithMany(w => w.Inventories) // Một Warehouse có nhiều bản ghi Inventory của các sản phẩm
			.HasForeignKey(i => i.WarehouseId)
			.OnDelete(DeleteBehavior.Restrict) // Dùng Restrict để tránh vô tình xóa kho làm mất diện rộng dữ liệu tồn kho
			.IsRequired();
			builder.HasQueryFilter(i => !i.IsDeleted);
		}
	}
}
