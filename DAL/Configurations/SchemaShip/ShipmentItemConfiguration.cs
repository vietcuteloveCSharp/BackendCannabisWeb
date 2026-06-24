namespace DAL.Configurations.SchemaShip
{
	public class ShipmentItemConfiguration : IEntityTypeConfiguration<ShipmentItem> 
	{
		public void Configure(EntityTypeBuilder<ShipmentItem> builder)
		{
			builder.ToTable("ShipmentItems","Ship");
			builder.HasKey(si => si.Id);

			builder.HasOne(si => si.Shipment)
				   .WithMany(s => s.Items)
				   .HasForeignKey(si => si.ShipmentId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(si => si.OrderItem)
				   .WithMany()
				   .HasForeignKey(si => si.OrderItemId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}


