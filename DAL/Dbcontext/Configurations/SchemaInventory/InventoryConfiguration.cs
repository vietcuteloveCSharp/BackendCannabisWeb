using DAL.Entities.Inventory;

namespace DAL.Dbcontext.Configurations.SchemaInventory
{
	internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
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
				.WithOne(v => v.Inventory)
				.HasForeignKey<Inventory>(i => i.ProductVariantId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasQueryFilter(i => !i.IsDeleted);
		}
	}
}
