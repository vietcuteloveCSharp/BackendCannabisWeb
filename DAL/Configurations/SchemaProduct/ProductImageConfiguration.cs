using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations.SchemaProduct
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.ToTable("ProductImages", "Products");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.ImageUrl).IsRequired();
			builder.Property(i => i.AltText).HasMaxLength(300);
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
