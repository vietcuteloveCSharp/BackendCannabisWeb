using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products", "Products");
			builder.HasKey(c => c.ProductId);
			builder.Property(c => c.ProductId).ValueGeneratedOnAdd();
			builder.Property(c => c.ProductName).HasMaxLength(255).IsRequired();
			builder.Property(p => p.ProductType).HasMaxLength(50);
			builder.Property<bool>(c => c.IsActive).HasDefaultValue(true).IsRequired();

			builder.HasOne(c => c.Category)
				   .WithMany(c => c.Products)
				   .HasForeignKey(c => c.CategoryId)
				   .HasConstraintName("FK_PRODUCT_CATEGORY_CATEGORYID")
				   .OnDelete(DeleteBehavior.Restrict)
				   .IsRequired();

			builder.HasOne(c => c.Brand)
				   .WithMany(c => c.Products)
				   .HasForeignKey(c => c.BrandId)
				   .HasConstraintName("FK_PRODUCT_BRAND_BRANDID")
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(c => c.ProductName).HasDatabaseName("IX_Product_ProductName").IsUnique();
		}
	}
}
