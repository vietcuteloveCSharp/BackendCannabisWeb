namespace DAL.Configurations.SchemaInventory
{
	public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
	{
		public void Configure(EntityTypeBuilder<Warehouse> builder)
		{
			builder.ToTable("Warehouses", "Inventory");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Name)
				  .IsRequired()
				  .HasMaxLength(150); 

			builder.Property(e => e.Address)
				  .IsRequired()
				  .HasMaxLength(500);

			// Cấu hình Global Query Filter cho Soft Delete (Tự động lọc các kho đã xóa)
			builder.HasQueryFilter(w => !w.IsDeleted);
		}
	}
}
