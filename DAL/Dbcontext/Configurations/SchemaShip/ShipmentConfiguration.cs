using DAL.Entities.Ship;

namespace DAL.Dbcontext.Configurations.SchemaShip
{
	public class ShipmentConfiguration :IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
		{
			builder.ToTable("Shipments","Ship");
			builder.HasKey(s => s.Id);
			builder.Property(c => c.TrackingNumber).HasMaxLength(50);
			builder.Property(c => c.ShippingFee).HasPrecision(10, 2);

			// FK Order
			builder.HasOne(s => s.Order)
				   .WithMany(o => o.Shipments)
				   .HasForeignKey(s => s.OrderId)
				   .OnDelete(DeleteBehavior.Cascade);

			// FK Status
			builder.HasOne(s => s.Status)
				   .WithMany(st => st.Shipments)
				   .HasForeignKey(s => s.StatusId)
				   .OnDelete(DeleteBehavior.Restrict);

			// FK ShippingMethod
			builder.HasOne(s => s.Method)
				   .WithMany(m => m.Shipments)
				   .HasForeignKey(s => s.MethodId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasQueryFilter(s => !s.IsDeleted);
		}
	}
}
