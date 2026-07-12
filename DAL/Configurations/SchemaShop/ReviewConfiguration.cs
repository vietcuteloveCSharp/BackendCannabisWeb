namespace DAL.Configurations.SchemaShop
{
	public class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.ToTable("Reviews", "Shop");

			builder.HasKey(r => r.Id);
			builder.Property(r => r.Id).ValueGeneratedOnAdd();

			builder.Property(r => r.ReviewTitle).HasMaxLength(200);
			builder.Property(r => r.Comments).HasMaxLength(2000);
			builder.Property(r => r.Rating).IsRequired();

			builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(r => r.IsDeleted).HasDefaultValue(false);

			builder.HasIndex(r => r.CustomerId);
			builder.HasIndex(r => r.ProductId);
			builder.HasIndex(r => r.OrderId);

			// Cấu hình quan hệ chặn Cascade chồng chéo từ Order/Product
			builder.HasOne(r => r.Customer)
				.WithMany(c => c.Reviews)
				.HasForeignKey(r => r.CustomerId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Reviews_Customers_CustomerId");

			builder.HasOne(r => r.Product)
				.WithMany(r=>r.Reviews) // Tùy thuộc cấu hình phía Product Entity của bạn
				.HasForeignKey(r => r.ProductId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Reviews_Products_ProductId");

			builder.HasOne(r => r.Order)
				.WithMany(o => o.Reviews)
				.HasForeignKey(r => r.OrderId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Reviews_Orders_OrderId");
		}
	}
}


