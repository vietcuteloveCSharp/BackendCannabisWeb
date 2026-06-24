namespace DAL.Configurations.SchemaInventory
{
	public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
	{
		public void Configure(EntityTypeBuilder<StockMovement> builder)
		{
			builder.ToTable("StockMovements","Inventory");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.QuantityChanged)
				.IsRequired();

			builder.Property(s => s.MovementType)
				.IsRequired();

			builder.Property(s => s.Note)
				.HasMaxLength(500);

			// FK Inventory
			builder.HasOne(s => s.Inventory)
				.WithMany(i => i.StockMovements)
				.HasForeignKey(s => s.InventoryId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasQueryFilter(s => !s.IsDeleted);
		}
	}
}
