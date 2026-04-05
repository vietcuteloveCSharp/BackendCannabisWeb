

namespace DAL.Dbcontext.Configurations.SchemaPromotion
{
	public class PromotionProductConfiguration : IEntityTypeConfiguration<PromotionProduct>
	{
		public void Configure(EntityTypeBuilder<PromotionProduct> builder)
		{
			builder.ToTable("PromotionProducts", "Promotions");
			builder.HasKey(pp => new { pp.PromotionId, pp.ProductId });

			builder.HasOne(c => c.Promotion)
				   .WithMany(c => c.Products)
				   .HasForeignKey(c => c.PromotionId)
				   .HasConstraintName("FK_PROMOTIONPRODUCT_PROMOTION_PROMOTIONID")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(c => c.Product)
				   .WithMany(c => c.PromotionProducts)
				   .HasForeignKey(c => c.ProductId)
				   .HasConstraintName("FK_PROMOTIONPRODUCT_PRODUCT_PRODUCTID")
				   .OnDelete(DeleteBehavior.Restrict);
			builder.HasQueryFilter(x => !x.Product.IsDeleted);
		}
	}
}
