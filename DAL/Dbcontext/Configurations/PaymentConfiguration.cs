using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.HasKey(c => c.PaymentId);
			builder.Property(c => c.PaymentId).ValueGeneratedOnAdd();
			builder.Property(c => c.PaymentName).HasMaxLength(300).IsRequired();
			builder.Property(c => c.Description).HasMaxLength(1000);

			builder.HasOne(c => c.Order)
				   .WithOne(c => c.Payment)
				   .HasForeignKey<Payment>(c => c.OrderId)
				   .HasConstraintName("FK_PAYMENT_ORDER_ORDERID")
				   .OnDelete(DeleteBehavior.Restrict)
				   .IsRequired();

			builder.HasIndex(c => c.OrderId).HasDatabaseName("IX_Payment_OrderId");
			builder.HasIndex(c => c.PaymentName).HasDatabaseName("IX_Payment_PaymentName");
		}
	}
}
