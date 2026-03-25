using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class SeedConfiguration : IEntityTypeConfiguration<Seed>
	{
		public void Configure(EntityTypeBuilder<Seed> builder)
		{
			builder.ToTable("Seeds", "Inventory");
			builder.HasKey(c => c.SeedId);
			builder.Property(c => c.SeedId).ValueGeneratedOnAdd();
			builder.Property(c => c.THCContent).HasPrecision(5, 2).IsRequired();
			builder.Property(c => c.CBDContent).HasPrecision(5, 2).IsRequired();
			builder.Property(c => c.StrainType).HasConversion<string>();
			builder.HasOne(c => c.Classification).WithMany(c => c.Seeds).HasForeignKey(c => c.ClassifyId).HasConstraintName("FK_SEED_CLASSIFICATION_CLASSIFYID").OnDelete(DeleteBehavior.Restrict).IsRequired();
			builder.Property(c => c.FloweringTimeDays).HasColumnType("INT");
			builder.Property(c => c.Yield).HasPrecision(10, 2);
			builder.Property(c => c.Difficulty).HasConversion<string>();
			builder.Property(c => c.Price).HasPrecision(10, 2).IsRequired();
			builder.Property(c => c.IndicaPercentage).HasPrecision(5, 2).IsRequired();
			builder.Property(c => c.SativaPercentage).HasPrecision(5, 2).IsRequired();
			builder.Property(c => c.TotalQuantity).HasColumnType("INT");
			builder.Property(c => c.Description).HasMaxLength(1000);
			builder.Property(c => c.ProductId).IsRequired();
			builder.HasOne(c => c.Product).WithOne(c => c.Seed).HasForeignKey<Seed>(c => c.ProductId).HasConstraintName("FK_SEED_PRODUCT_PRODUCTID").OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(s => s.Breeder)
				.WithMany(b => b.Seeds)
				.HasForeignKey(s => s.BreederId)
				.OnDelete(DeleteBehavior.Restrict);
			builder.HasIndex(x => x.ProductId).IsUnique();

			// 2. Cấu hình các trường mới bổ sung
			builder.Property(c => c.Genetics).HasMaxLength(255); // Giới hạn để tối ưu DB
			builder.Property(c => c.IndoorHeightCm).HasColumnType("INT");
			builder.HasIndex(x => x.StrainType).HasDatabaseName("IX_SEEDS_STRAINTYPE"); // Filter cực nhiều
			builder.HasIndex(x => x.THCContent).HasDatabaseName("IX_SEEDS_THC"); // Filter theo độ mạnh
			builder.HasIndex(x => x.Price).HasDatabaseName("IX_SEEDS_PRICE");       // Filter theo giá tiền
			builder.HasIndex(x => x.ProductId).IsUnique().HasDatabaseName("IX_SEED_PRODUCTID");
		}
	}
}
