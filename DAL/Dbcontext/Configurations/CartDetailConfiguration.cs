using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetails>
	{
		public void Configure(EntityTypeBuilder<CartDetails> builder)
		{
			builder.ToTable("CartDetails", "Orders");
			builder.HasKey(cd => cd.CartDetailsId);

			builder.Property(cd => cd.Price).HasPrecision(10, 2);
			builder.Property(cd => cd.Quantity).IsRequired();

			builder.Property(cd => cd.CartId).IsRequired();
			builder.HasOne(cd => cd.Cart)
				  .WithMany(c => c.CartDetails)
				  .HasForeignKey(cd => cd.CartId)
				  .HasConstraintName("FK_CARTDETAILS_CART_CARTID")
				  .OnDelete(DeleteBehavior.Cascade);

			builder.Property(cd => cd.ProductId).IsRequired();
			builder.HasOne(cd => cd.Product)
				  .WithMany(p => p.CartsDetails)
				  .HasForeignKey(cd => cd.ProductId)
				  .HasConstraintName("FK_CARTDETAILS_PRODUCT_PRODUCTID")
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(cd => cd.CartId)
				  .HasDatabaseName("IX_CartDetails_CartId");

			builder.HasIndex(cd => cd.ProductId)
				  .HasDatabaseName("IX_CartDetails_ProductId");
		}
	}
}
