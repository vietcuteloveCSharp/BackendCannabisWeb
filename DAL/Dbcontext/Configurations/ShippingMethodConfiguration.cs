using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
	{
		public void Configure(EntityTypeBuilder<ShippingMethod> builder)
		{
			builder.HasKey(c => c.ShippingId);
			builder.Property(c => c.ShippingId).ValueGeneratedOnAdd();
			builder.Property(c => c.Name).HasMaxLength(150).IsRequired();
			builder.Property(c => c.Carrier).HasMaxLength(150).IsRequired();
			builder.Property(c => c.EstimatedDeliveryDate).HasColumnType("datetime2").IsRequired();
			builder.Property(c => c.EstimatedDeliveryDays).HasColumnType("int").HasDefaultValue(0).IsRequired();

			builder.HasOne(c => c.Order)
				   .WithOne(c => c.ShippingMethod)
				   .HasForeignKey<ShippingMethod>(c => c.OrderId)
				   .HasConstraintName("FK_SHIPPINGMETHOD_ORDER_ORDERID")
				   .IsRequired();

			builder.HasIndex(c => c.OrderId).HasDatabaseName("IX_ShippingMethod_OrderId");
		}
	}
}
