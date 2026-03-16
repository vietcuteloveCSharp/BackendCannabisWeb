using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class PromotionProductConfiguration : IEntityTypeConfiguration<PromotionProduct>
	{
		public void Configure(EntityTypeBuilder<PromotionProduct> builder)
		{
			builder.HasKey(pp => new { pp.PromotionId, pp.ProductId });

			builder.HasOne(c => c.Promotion)
				   .WithMany(c => c.PromotionProducts)
				   .HasForeignKey(c => c.PromotionId)
				   .HasConstraintName("FK_PROMOTIONPRODUCT_PROMOTION_PROMOTIONID")
				   .IsRequired();

			builder.HasOne(c => c.Product)
				   .WithMany(c => c.PromotionProducts)
				   .HasForeignKey(c => c.ProductId)
				   .HasConstraintName("FK_PROMOTIONPRODUCT_PRODUCT_PRODUCTID")
				   .IsRequired();
		}
	}
}
