using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class PromotionCategoryConfiguration : IEntityTypeConfiguration<PromotionCategory>
	{
		public void Configure(EntityTypeBuilder<PromotionCategory> builder)
		{
			builder.HasKey(pc => new { pc.PromotionId, pc.CategoryId });

			builder.HasOne(c => c.Promotion)
				.WithMany(c => c.PromotionCategories)
				.HasForeignKey(c => c.PromotionId)
				.HasConstraintName("FK_PROMOTIONCATEGORY_PROMOTION_PROMOTIONID")
				.IsRequired();

			builder.HasOne(c => c.Category)
				.WithMany(c => c.PromotionCategories)
				.HasForeignKey(c => c.CategoryId)
				.HasConstraintName("FK_PROMOTIONCATEGORY_CATEGORY_CATEGORYID")
				.IsRequired();
		}
	}
}
