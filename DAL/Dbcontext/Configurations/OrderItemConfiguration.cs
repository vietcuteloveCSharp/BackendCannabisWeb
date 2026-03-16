using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(c => c.OrderItemId);
			builder.Property(c => c.OrderItemId).ValueGeneratedOnAdd();
			builder.Property<int>(c => c.Quantity);
			builder.Property(c => c.Price).HasPrecision(10, 2);

			builder.HasOne(c => c.Order)
				   .WithMany(c => c.OrderItems)
				   .HasForeignKey(c => c.OrderId)
				   .HasConstraintName("FK_ORDERITEM_ORDER")
				   .OnDelete(DeleteBehavior.Cascade)
				   .IsRequired();

			builder.HasOne(c => c.Product)
				   .WithMany(c => c.OrderItems)
				   .HasForeignKey(c => c.ProductId)
				   .HasConstraintName("FK_ORDERITEM_PRODUCT")
				   .OnDelete(DeleteBehavior.Restrict)
				   .IsRequired();
		}
	}
}
