namespace DAL.Configurations.SchemaShop
{
	public class PaymentStatusConfiguration :IEntityTypeConfiguration<PaymentStatus>
	{
		public void Configure(EntityTypeBuilder<PaymentStatus> builder) 
		{
			builder.ToTable("PaymentStatuses","Shop");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(s => s.Description)
				   .HasMaxLength(200);
		}
	}
}
