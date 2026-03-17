using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class CarbonFilterConfiguration : IEntityTypeConfiguration<CarbonFilter>
	{
		public void Configure(EntityTypeBuilder<CarbonFilter> builder)
		{
			builder.ToTable("CarbonFilters", "Inventory");
			builder.HasKey(cf => cf.CarbonFilterId);

			builder.Property(cf => cf.AirflowRate).HasMaxLength(150);
			builder.Property(cf => cf.Price).HasPrecision(10, 2);
			builder.Property(cf => cf.Description).HasMaxLength(1000);

			// Brand (OK rồi)
			builder.Property(cf => cf.BrandId).IsRequired();

			builder.HasOne(cf => cf.Brand)
				  .WithMany(b => b.CarbonFilters)
				  .HasForeignKey(cf => cf.BrandId)
				  .OnDelete(DeleteBehavior.Restrict)
				  .HasConstraintName("FK_CARBONFILTER_BRAND_BRANDID");

			// ✅ FIX 1-1 Product
			builder.HasOne(cf => cf.Product)
				  .WithOne(p => p.CarbonFilter)
				  .HasForeignKey<CarbonFilter>(cf => cf.ProductId)
				  .OnDelete(DeleteBehavior.Cascade)
				  .IsRequired()
				  .HasConstraintName("FK_CARBONFILTER_PRODUCT_PRODUCTID");

			// ✅ Index
			builder.HasIndex(cf => cf.BrandId)
				  .HasDatabaseName("IX_CarbonFilters_BrandId");

			builder.HasIndex(cf => cf.ProductId)
				  .IsUnique() // 🔥 QUAN TRỌNG
				  .HasDatabaseName("IX_CarbonFilter_ProductId");
		}
	}
}
