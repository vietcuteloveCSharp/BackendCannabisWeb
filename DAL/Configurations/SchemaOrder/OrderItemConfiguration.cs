namespace DAL.Configurations.SchemaOrder
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems", "Orders");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.Quantity);
			builder.Property(c => c.UnitPrice).HasPrecision(18, 2);

			builder.HasQueryFilter(oi => !oi.IsDeleted);

			builder.HasOne(c => c.Order)
				   .WithMany(c => c.OrderItems)
				   .HasForeignKey(c => c.OrderId)
				   .HasConstraintName("FK_ORDERITEM_ORDER")
				   .OnDelete(DeleteBehavior.Cascade)
				   .IsRequired();
			builder.HasOne(oi => oi.ProductVariant)
				.WithMany(c=>c.OrderItems)
				.HasForeignKey(oi => oi.ProductVariantId)
				.OnDelete(DeleteBehavior.Restrict);
			
		}
	}
}
