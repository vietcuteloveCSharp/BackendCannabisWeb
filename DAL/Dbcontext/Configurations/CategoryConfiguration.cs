using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories", "Products");

			builder.HasKey(c => c.CategoryId);

			builder.Property(c => c.CategoryName).HasMaxLength(100).IsRequired();

			builder.HasMany(c => c.Products)
				  .WithOne(p => p.Category)
				  .HasForeignKey(p => p.CategoryId)
				  .OnDelete(DeleteBehavior.Restrict)
				  .HasConstraintName("FK_PRODUCT_CATEGORY_CATEGORYID");

			builder.HasMany(c => c.PromotionCategories)
				  .WithOne(pc => pc.Category)
				  .HasForeignKey(pc => pc.CategoryId)
				  .OnDelete(DeleteBehavior.Cascade)
				  .HasConstraintName("FK_PROMOTIONCATEGORY_CATEGORY_CATEGORYID");

			builder.HasIndex(c => c.CategoryName)
				  .IsUnique()
				  .HasDatabaseName("IX_Categories_CategoryName");
		}
	}
}
