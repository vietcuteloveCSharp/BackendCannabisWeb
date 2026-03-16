using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class DehumidifierConfiguration : IEntityTypeConfiguration<Dehumidifier>
	{
		public void Configure(EntityTypeBuilder<Dehumidifier> builder)
		{
			builder.HasKey(d => d.DehumidifierId);
			builder.Property(d => d.DehumidificationCapacity).HasColumnType("decimal(5,2)");
			builder.Property(d => d.CoverageArea).HasColumnType("decimal(10,2)");
			builder.Property(d => d.NoiseLevel).HasColumnType("decimal(5,2)");
			builder.Property(d => d.PowerConsumption).HasColumnType("decimal(10,2)");
			builder.Property(d => d.Description).HasMaxLength(1000);
			builder.Property(d => d.BrandId).IsRequired();

			builder.HasOne(d => d.Brand)
				  .WithMany(b => b.Dehumidifiers)
				  .HasForeignKey(d => d.BrandId)
				  .HasConstraintName("FK_DEHUMIDIFIERS_BRAND_BRANDID")
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(d => d.Product)
				  .WithOne(p => p.Dehumidifier)
				  .HasForeignKey<Dehumidifier>(d => d.ProductId)
				  .HasConstraintName("FK_DEHUMIDIFIERS_PRODUCT_PRODUCTID")
				  .OnDelete(DeleteBehavior.Cascade)
				  .IsRequired();

			builder.HasIndex(g => g.ProductId).HasDatabaseName("IX_Dehumidifier_ProductId");
		}
	}
}
