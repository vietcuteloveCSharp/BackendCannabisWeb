using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
	{
		public void Configure(EntityTypeBuilder<Promotion> builder)
		{
			builder.ToTable("Promotions", "Promotions");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.PromotionName).HasColumnType("NVARCHAR(255)").IsRequired();
			builder.Property(c => c.Description).HasMaxLength(1000);
			builder.Property(c => c.DiscountType).HasConversion<string>().IsRequired();
			builder.Property(c => c.DiscountValue).HasPrecision(12, 2).IsRequired();
			builder.Property(c => c.MinimumOrderValue).HasPrecision(12, 2).IsRequired();
			builder.Property(c => c.MaximumDiscountValue).HasPrecision(12, 2).IsRequired();
			builder.Property(c => c.MinimumQuantity).IsRequired();
			builder.Property(a => a.StartDate).HasDefaultValueSql("GETUTCDATE()").IsRequired();
			builder.Property(a => a.EndDate).HasDefaultValueSql("GETUTCDATE()").IsRequired();
			builder.Property(a => a.IsActive).HasDefaultValue(true).IsRequired();
			builder.HasIndex(c => c.PromotionName).HasDatabaseName("IX_Promotion_PromotionName");
		}
	}
}
