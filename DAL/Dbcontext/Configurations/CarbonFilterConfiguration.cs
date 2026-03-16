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
			builder.HasKey(cf => cf.CarbonFilterId);
			builder.Property(cf => cf.AirflowRate).HasMaxLength(150);
			builder.Property(cf => cf.Price).HasColumnType("decimal(10,2)");
			builder.Property(cf => cf.Description).HasMaxLength(1000);
			builder.Property(cf => cf.BrandId).IsRequired();
			builder.HasOne(cf => cf.Brand)
				  .WithMany(b => b.CarbonFilters)
				  .HasForeignKey(cf => cf.BrandId)
				  .OnDelete(DeleteBehavior.Restrict)
				  .HasConstraintName("FK_CARBONFILTER_BRAND_BRANDID");

			builder.HasOne(cf => cf.Product)
				  .WithOne(p => p.CarbonFilter)
				  .HasForeignKey<CarbonFilter>(cf => cf.CarbonFilterId)
				  .OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(cf => cf.BrandId)
				  .HasDatabaseName("IX_CarbonFilters_BrandId");
			builder.HasOne(d => d.Product)
				  .WithOne(p => p.CarbonFilter)
				  .HasForeignKey<CarbonFilter>(d => d.ProductId)
				  .HasConstraintName("FK_CARBONFILTER_PRODUCT_PRODUCTID")
				  .OnDelete(DeleteBehavior.Cascade)
				  .IsRequired();
			builder.HasIndex(g => g.ProductId)
				.HasDatabaseName("IX_CarbonFilter_ProductId");
		}
	}
}
