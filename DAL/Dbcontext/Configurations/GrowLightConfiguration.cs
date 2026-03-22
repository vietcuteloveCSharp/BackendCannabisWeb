using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class GrowLightConfiguration : IEntityTypeConfiguration<GrowLight>
	{
		public void Configure(EntityTypeBuilder<GrowLight> builder)
		{
			builder.ToTable("GrowLights", "Inventory");
			builder.HasKey(gl => gl.GrowLightId);
			builder.Property(gl => gl.BrandId).IsRequired();
			builder.Property(gl => gl.Quantity).IsRequired();
			builder.Property(gl => gl.Wattage).IsRequired();
			builder.Property(gl => gl.Price).HasPrecision(10, 2).IsRequired();
			builder.Property(gl => gl.CoverageArea).IsRequired();
			builder.Property(gl => gl.WarrantyPeriod).IsRequired();
			builder.Property(gl => gl.PowerSupplyId).IsRequired();
			builder.Property(gl => gl.ChipModelId).IsRequired();
			builder.Property(gl => gl.CoolingSystemId).IsRequired();
			builder.Property(gl => gl.SpectrumId).IsRequired();
			builder.Property(gl => gl.Lifespan).IsRequired();
			builder.Property(gl => gl.ModelNumber).HasMaxLength(100).IsRequired();
			builder.Property(gl => gl.Description).HasMaxLength(1000);

			builder.HasOne(gl => gl.Brand).WithMany(b => b.GrowLights).HasForeignKey(gl => gl.BrandId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_GROWLIGHT_BRAND");
			builder.HasOne(gl => gl.PowerSupply).WithMany(ps => ps.GrowLights).HasForeignKey(gl => gl.PowerSupplyId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_GROWLIGHT_POWERSUPPLY");
			builder.HasOne(gl => gl.ChipModel).WithMany(cm => cm.GrowLights).HasForeignKey(gl => gl.ChipModelId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_GROWLIGHT_CHIPMODEL");
			builder.HasOne(gl => gl.CoolingSystem).WithMany(cs => cs.GrowLights).HasForeignKey(gl => gl.CoolingSystemId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_GROWLIGHT_COOLINGSYSTEM");
			builder.HasOne(gl => gl.Spectrum).WithMany(s => s.GrowLights).HasForeignKey(gl => gl.SpectrumId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_GROWLIGHT_SPECTRUM");

			builder.HasOne(d => d.Product).WithOne(p => p.GrowLight).HasForeignKey<GrowLight>(d => d.ProductId).HasConstraintName("FK_GROWLIGHT_PRODUCT_PRODUCTID").OnDelete(DeleteBehavior.Cascade).IsRequired();

			builder.HasIndex(gl => gl.BrandId).HasDatabaseName("IX_GrowLights_BrandId");
			builder.HasIndex(gl => gl.ChipModelId).HasDatabaseName("IX_GrowLights_ChipModelId");
			builder.HasIndex(gl => gl.PowerSupplyId).HasDatabaseName("IX_GrowLights_PowerSupplyId");
			builder.HasIndex(gl => gl.CoolingSystemId).HasDatabaseName("IX_GrowLights_CoolingSystemId");
			builder.HasIndex(gl => gl.SpectrumId).HasDatabaseName("IX_GrowLights_SpectrumId");
			builder.HasIndex(g => g.ProductId).HasDatabaseName("IX_Growlight_ProductId");
		}
	}
}
