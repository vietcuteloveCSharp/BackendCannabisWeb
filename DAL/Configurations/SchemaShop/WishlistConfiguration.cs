namespace DAL.Configurations.SchemaShop
{
	public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
		{
			builder.ToTable("Wishlists", "Shop");

			builder.HasKey(w => w.Id);
			builder.Property(w => w.Id).ValueGeneratedOnAdd();

			builder.Property(w => w.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(w => w.IsDeleted).HasDefaultValue(false);

			// 🔒 Bảo mật tầng DB: Khách hàng không thể thêm trùng 1 sản phẩm nhiều lần vào Wishlist
			builder.HasIndex(w => new { w.CustomerId, w.ProductId }).IsUnique();

			builder.HasOne(w => w.Customer)
				.WithMany(c => c.Wishlists)
				.HasForeignKey(w => w.CustomerId) // Đang khớp theo thuộc tính hiện tại của bạn
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Wishlists_Customers_CustomerId");

			builder.HasOne(w => w.Product)
				.WithMany(w=>w.Wishlists) // Giả định bảng Product không cần List<Wishlist> đảo ngược
				.HasForeignKey(w => w.ProductId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Wishlists_Products_ProductId");
		}
	}

}
