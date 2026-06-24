namespace DAL.Configurations.SchemaOrder
{
	internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
	{
		public void Configure(EntityTypeBuilder<OrderStatus> builder)
		{
			builder.ToTable("OrderStatuses","Orders");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(s => s.Description)
				.HasMaxLength(200);
		}
	}

}
