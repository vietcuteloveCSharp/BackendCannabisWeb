using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(c => c.OrderId);
			builder.Property(c => c.OrderId).ValueGeneratedOnAdd();

			builder.HasOne(c => c.Buyer).WithMany(c => c.OrdersAsBuyer).HasForeignKey(c => c.BuyerId).HasConstraintName("FK_ORDER_BUYER_BUYERID").OnDelete(DeleteBehavior.Restrict).IsRequired();
			builder.HasOne(c => c.Seller).WithMany(c => c.OrdersAsSeller).HasForeignKey(c => c.SellerId).HasConstraintName("FK_ORDER_SELLER_SELLERID").OnDelete(DeleteBehavior.Restrict).IsRequired();

			builder.Property(c => c.OrderStatus).HasConversion<string>().IsRequired();
			builder.Property(c => c.TotalAmount).HasPrecision(10, 2).IsRequired();
			builder.Property(c => c.TrackingNumber).HasMaxLength(50).IsRequired();
			builder.Property(c => c.ShippingFee).HasPrecision(10, 2);
			builder.Property(c => c.ShippingAddress).HasMaxLength(2000);
		}
	}
}
