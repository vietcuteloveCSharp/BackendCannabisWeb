using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations.SchemaPromotion
{
	public class PromotionCategoryConfiguration : IEntityTypeConfiguration<PromotionCategory>
	{
		public void Configure(EntityTypeBuilder<PromotionCategory> builder)
		{
			builder.ToTable("PromotionsCategories", "Promotions");
			builder.HasKey(pc => new { pc.PromotionId, pc.CategoryId });

			builder.HasOne(c => c.Promotion)
				.WithMany(c => c.Categories)
				.HasForeignKey(c => c.PromotionId)
				.IsRequired();

			builder.HasOne(c => c.Category)
				.WithMany(c => c.Promotions)
				.HasForeignKey(c => c.CategoryId)
				.IsRequired();
			builder.HasQueryFilter(oi => !oi.IsDeleted);
		}
	}
}
