namespace DAL.Configurations.SchemaShop
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems", "Shop");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.Quantity);
			builder.Property(c => c.UnitPrice).HasPrecision(18, 2);
			builder.Property(oi => oi.ProductNameSnapshot)
				.IsRequired()
				.HasMaxLength(250); // Giới hạn độ dài để tối ưu lưu trữ hóa đơn

			builder.Property(oi => oi.VariantNameSnapshot)
				.HasMaxLength(250);
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
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_OrderItems_ProductVariants_ProductVariantId");

		}
	}
}
