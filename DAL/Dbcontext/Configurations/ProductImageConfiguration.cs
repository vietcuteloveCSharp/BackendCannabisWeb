using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.ToTable("ProductImages", "Products");
			builder.HasKey(c => c.ProductImageId);
			builder.Property(c => c.ProductImageId).ValueGeneratedOnAdd();
			builder.Property<string>(c => c.ImageUrl).IsRequired();
			builder.Property(c => c.IsMainImage).HasDefaultValue(false);

			builder.HasOne(c => c.Product)
				   .WithMany(c => c.ProductImages)
				   .HasForeignKey(c => c.ProductId)
				   .HasConstraintName("FK_PRODUCTIMAGE_PRODUCT_PRODUCTID")
				   .OnDelete(DeleteBehavior.Cascade)
				   .IsRequired();
		}
	}
}
