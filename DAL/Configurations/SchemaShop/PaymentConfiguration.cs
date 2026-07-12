namespace DAL.Configurations.SchemaShop
{
	public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.ToTable("Payments", "Shop");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c=>c.Amount).HasPrecision(18,2);
			builder.Property(c => c.TransactionId).HasMaxLength(100);

			builder.HasOne(c => c.Order)
				   .WithOne(c => c.Payment)
				   .HasForeignKey<Payment>(c => c.OrderId)
				   .HasConstraintName("FK_PAYMENT_ORDER_ORDERID")
				   .OnDelete(DeleteBehavior.Restrict)
				   .IsRequired();
			builder.HasOne(p => p.PaymentMethod)
			   .WithMany(m => m.Payments)
			   .HasForeignKey(p => p.PaymentMethodId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(p => p.PaymentStatus)
				.WithMany(s => s.Payments)
				.HasForeignKey(p => p.PaymentStatusId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(c => c.OrderId).HasDatabaseName("IX_Payment_OrderId");
			builder.HasIndex(c => c.PaymentStatusId).HasDatabaseName("IX_Payment_PaymentStatusId");
			builder.HasIndex(c => c.PaymentMethodId).HasDatabaseName("IX_Payment_PaymentMethodId");
		}
	}
}
