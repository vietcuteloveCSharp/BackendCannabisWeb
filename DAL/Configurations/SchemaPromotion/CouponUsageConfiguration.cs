namespace DAL.Configurations.SchemaPromotion
{
	public class CouponUsageConfiguration :IEntityTypeConfiguration<CouponUsage>
	{
		public void Configure(EntityTypeBuilder<CouponUsage> builder)
		{
			builder.ToTable("CouponUsages", "Promotions");

			builder.HasKey(cu => cu.Id);
			builder.Property(cu => cu.Id).ValueGeneratedOnAdd();
			builder.HasIndex(cu => cu.CouponId);
			builder.HasIndex(cu => cu.CustomerId);
			builder.HasIndex(cu => cu.OrderId);

						builder.HasQueryFilter(oi => !oi.IsDeleted);
			// 1. Liên kết với Coupon
			builder.HasOne(cu => cu.Coupon)
				.WithMany(c=>c.Usages) // Nếu bảng Coupon không cần bộ sưu tập CouponUsages ngược lại
				.HasForeignKey(cu => cu.CouponId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_CouponUsages_Coupons_CouponId");

			// 2. Liên kết với Customer
			builder.HasOne(cu => cu.Customer)
				.WithMany() // Hoặc c.CouponUsages nếu bạn có khai báo trong Customer.cs
				.HasForeignKey(cu => cu.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_CouponUsages_Customers_CustomerId");

			// 3. Liên kết với Order
			// Dùng Restrict/NoAction ở đây để tránh lỗi Multiple Cascade Paths từ SQL Server 
			// (Vì nếu xóa Customer -> Xóa Order -> Xóa CouponUsage, đường truyền sẽ bị trùng lặp)
			builder.HasOne(cu => cu.Order)
				.WithMany()
				.HasForeignKey(cu => cu.OrderId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_CouponUsages_Orders_OrderId");
		}
	}
}
