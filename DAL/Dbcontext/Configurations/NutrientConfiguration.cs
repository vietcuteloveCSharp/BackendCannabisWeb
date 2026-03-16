using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class NutrientConfiguration : IEntityTypeConfiguration<Nutrient>
	{
		public void Configure(EntityTypeBuilder<Nutrient> builder)
		{
			builder.HasKey(c => c.NutrientId);
			builder.Property(c => c.NutrientId).ValueGeneratedOnAdd();

			builder.HasOne(c => c.Brand)
				.WithMany(c => c.Nutrients)
				.HasForeignKey(c => c.BrandId)
				.HasConstraintName("FK_NUTRIENT_BRAND_BRANDID")
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.NutrientType)
				.WithMany(c => c.Nutrients)
				.HasForeignKey(c => c.NutrientTypeId)
				.HasConstraintName("FK_NUTRIENT_NUTRIENTTYPE_NUTRIENTTYPEID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.Property<int>(c => c.Quantity).IsRequired();
			builder.Property(c => c.Price).HasPrecision(10, 2).IsRequired();
			builder.Property<int>(c => c.VolumeMl).IsRequired();
			builder.Property(c => c.Ingredients).HasMaxLength(255);
			builder.Property(c => c.NpkRatio).HasMaxLength(50);
			builder.Property<bool>(c => c.IsOrganic).HasDefaultValue(false);
			builder.Property(c => c.Description).HasMaxLength(1000);
			builder.Property(c => c.StorageInstructions).HasMaxLength(255);

			builder.HasIndex(c => c.BrandId).HasDatabaseName("IX_Nutrient_BrandId");
			builder.HasIndex(c => c.NutrientTypeId).HasDatabaseName("IX_Nutrient_NutrientTypeId");

			builder.Property(c => c.ProductId).IsRequired();
			builder.HasOne(c => c.Product)
				.WithOne(c => c.Nutrient)
				.HasForeignKey<Nutrient>(c => c.ProductId)
				.HasConstraintName("FK_NUTRIENT_PRODUCT_PRODUCTID")
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(g => g.ProductId).HasDatabaseName("IX_Nutrient_ProductId");
		}
	}
}
