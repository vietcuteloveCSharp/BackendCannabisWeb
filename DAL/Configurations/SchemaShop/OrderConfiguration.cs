using DAL.Entities.Shop;

namespace DAL.Configurations.SchemaShop
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders", "Shop");

			builder.HasKey(o => o.Id);
			builder.Property(o => o.Id).ValueGeneratedOnAdd();

			builder.Property(o => o.TotalAmount)
				.IsRequired()
				.HasColumnType("decimal(18,2)"); 

            builder.Property(o => o.ShippingAddress)
				.IsRequired()
				.HasMaxLength(500); 

            builder.Property(o => o.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(o => o.IsDeleted).HasDefaultValue(false); 

            builder.HasIndex(o => o.CustomerId);
            builder.HasIndex(o => o.StaffId); 

            // Luồng 1: Người mua (Buyer - Customer)
            builder.HasOne(o => o.Buyer)
				.WithMany(c => c.OrdersAsBuyer)
				.HasForeignKey(o => o.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Orders_Customers_CustomerId");

			// Luồng 2: Nhân viên duyệt đơn (Staff) 
			// Dùng Restrict để khi xóa tài khoản Staff, lịch sử đơn hàng cũ vẫn được giữ lại nguyên vẹn
			builder.HasOne(o => o.Staff)
				.WithMany(s => s.OrdersAsStaff)
				.HasForeignKey(o => o.StaffId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Orders_Staffs_StaffId");

			builder.HasOne(o => o.OrderStatus)
				.WithMany(o=>o.Orders) // Giả định bảng OrderStatus không cần danh sách Order ngược lại
				.HasForeignKey(o => o.StatusId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Orders_OrderStatuses_StatusId");
		}
	}
}
