namespace DAL.Configurations.SchemaOrder
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders", "Orders");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();

			builder.Property(c => c.StatusId).IsRequired();
			builder.Property(c => c.TotalAmount).HasPrecision(10, 2).IsRequired();

			builder.Property(c => c.ShippingAddress).HasMaxLength(2000);

			builder.HasOne(c => c.Buyer)
				.WithMany(c => c.OrdersAsBuyer)
				.HasForeignKey(c => c.BuyerId)
				.HasConstraintName("FK_ORDER_BUYER_BUYERID")
				.OnDelete(DeleteBehavior.Restrict).IsRequired();

			builder.HasOne(c => c.Staff)
				.WithMany(c => c.OrdersAsStaff)
				.HasForeignKey(c => c.StaffId)
				.HasConstraintName("FK_ORDER_SELLER_StaffId")
				.OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(o => o.OrderStatus)
			  .WithMany(s => s.Orders)
			  .HasForeignKey(o => o.StatusId)
			  .OnDelete(DeleteBehavior.Restrict);
			builder.HasQueryFilter(o => !o.IsDeleted);
		}
	}
}
