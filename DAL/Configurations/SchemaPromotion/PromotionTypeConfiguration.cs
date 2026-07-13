using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations.SchemaPromotion
{
	public class PromotionTypeConfiguration : IEntityTypeConfiguration<PromotionType>
	{
		public void Configure(EntityTypeBuilder<PromotionType> builder)
		{
			builder.ToTable("PromotionTypes", "Promotions");
			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).ValueGeneratedOnAdd();

			builder.Property(t => t.Code).IsRequired().HasMaxLength(50);
			builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
		}
	}
}
