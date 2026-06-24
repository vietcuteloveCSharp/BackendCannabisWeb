namespace DAL.Configurations.SchemaPayment
{
	internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
	{
		public void Configure(EntityTypeBuilder<PaymentMethod> builder)
		{
			builder.ToTable("PaymentMethods", "Payments");

			builder.HasKey(m => m.Id);

			builder.Property(m => m.Name)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(m => m.Description)
				   .HasMaxLength(200);
		}
	}
}
