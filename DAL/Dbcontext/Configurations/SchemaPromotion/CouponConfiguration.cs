
namespace DAL.Dbcontext.Configurations.SchemaPromotion
{
	internal class CouponConfiguration : IEntityTypeConfiguration<Coupon>
	{
		public void Configure(EntityTypeBuilder<Coupon> builder)
		{
			builder.ToTable("Coupons", "Promotions");

			builder.HasKey(c => c.Id);

			builder.Property(c => c.Code)
				   .IsRequired()
				   .HasMaxLength(50);
			builder.Property(c => c.DiscountAmount).HasPrecision(10, 2);
			builder.Property(c => c.MinOrderAmount).HasPrecision(10, 2);

			builder.HasOne(c => c.Promotion)
				   .WithMany(p => p.Coupons)
				   .HasForeignKey(c => c.PromotionId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasQueryFilter(c => !c.IsDeleted);
		}
	}
	
}
