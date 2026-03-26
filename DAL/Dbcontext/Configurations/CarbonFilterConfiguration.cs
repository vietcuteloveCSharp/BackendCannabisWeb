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
			builder.HasKey(cf => cf.Id);

			// --- Cấu hình thông số kỹ thuật (Kỹ thuật số hóa) ---
			builder.Property(cf => cf.AirflowRateCFM).IsRequired();
			builder.Property(cf => cf.FlangeSizeInch).HasPrecision(4, 1).IsRequired(); // Ví dụ: 6.5 inch
			builder.Property(cf => cf.CarbonBedThicknessMm).HasPrecision(5, 2);
			builder.Property(cf => cf.Price).HasPrecision(10, 2);
			builder.Property(cf => cf.Description).HasMaxLength(1000);
			builder.Property(cf => cf.Diameter).HasPrecision(6, 2);
			builder.Property(cf => cf.Length).HasPrecision(6, 2);
			// Nhiệt độ cần chính xác hơn bản cũ (decimal 3,2 là quá hẹp)
			builder.Property(cf => cf.MinTemperature).HasPrecision(5, 2);
			builder.Property(cf => cf.MaxTemperature).HasPrecision(5, 2);
			builder.Property(cf => cf.ModelNumber).HasMaxLength(100).IsRequired();
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
			builder.HasIndex(cf => cf.FlangeSizeInch).HasDatabaseName("IX_CarbonFilter_FlangeSize"); // User lọc theo kích cỡ quạt
			builder.HasIndex(cf => cf.AirflowRateCFM).HasDatabaseName("IX_CarbonFilter_Airflow");    // Lọc theo công suất hút
		}
	}
}
