namespace DAL.Configurations.SchemaPayment
{
	public class PaymentStatusConfiguration :IEntityTypeConfiguration<PaymentStatus>
	{
		public void Configure(EntityTypeBuilder<PaymentStatus> builder) 
		{
			builder.ToTable("PaymentStatuses","Payments");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(s => s.Description)
				   .HasMaxLength(200);
		}
	}
}
