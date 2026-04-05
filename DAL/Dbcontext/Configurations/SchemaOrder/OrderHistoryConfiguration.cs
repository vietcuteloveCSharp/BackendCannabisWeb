namespace DAL.Dbcontext.Configurations.SchemaOrder
{
	public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
	{
		public void Configure(EntityTypeBuilder<OrderHistory> builder)
		{
			builder.ToTable("OrderHistories","Orders");

			builder.HasKey(h => h.Id);
			builder.Property(c => c.Note).HasMaxLength(500);
			builder.HasQueryFilter(o => !o.Order.IsDeleted);

			builder.HasOne(h => h.Order)
				.WithMany(o => o.OrderHistories)
				.HasForeignKey(h => h.OrderId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(h => h.Status)
				.WithMany()
				.HasForeignKey(h => h.StatusId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}

