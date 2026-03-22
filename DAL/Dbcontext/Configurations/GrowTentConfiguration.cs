using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class GrowTentConfiguration : IEntityTypeConfiguration<GrowTent>
	{
		public void Configure(EntityTypeBuilder<GrowTent> builder)
		{
			builder.ToTable("GrowTents", "Inventory");
			builder.HasKey(gt => gt.GrowtentId);
			builder.Property(gt => gt.BrandId).IsRequired();
			builder.Property(gt => gt.Dimensions).HasMaxLength(100).IsRequired();
			builder.Property(gt => gt.Material).HasMaxLength(255).IsRequired();
			builder.Property(gt => gt.Waterproof).HasDefaultValue(false);
			builder.Property(gt => gt.Quantity).IsRequired();
			builder.Property(gt => gt.Price).HasPrecision(10, 2).IsRequired();
			builder.Property(gt => gt.FrameMaterial).HasMaxLength(255).IsRequired();
			builder.Property(gt => gt.WarrantyPeriod).IsRequired();
			builder.Property(gt => gt.Description).HasMaxLength(1000);

			builder.HasOne(gt => gt.Brand)
				  .WithMany(b => b.GrowTents)
				  .HasForeignKey(gt => gt.BrandId)
				  .OnDelete(DeleteBehavior.Restrict)
				  .HasConstraintName("FK_GROWTENT_BRAND_BRANDID");

			builder.HasOne(d => d.Product)
				  .WithOne(p => p.GrowTent)
				  .HasForeignKey<GrowTent>(d => d.ProductId)
				  .HasConstraintName("FK_GROWTENT_PRODUCT_PRODUCTID")
				  .OnDelete(DeleteBehavior.Cascade)
				  .IsRequired();

			builder.HasIndex(gt => gt.BrandId).HasDatabaseName("IX_GrowTents_BrandId");
			builder.HasIndex(g => g.ProductId).HasDatabaseName("IX_Growtent_ProductId");
		}
	}
}
