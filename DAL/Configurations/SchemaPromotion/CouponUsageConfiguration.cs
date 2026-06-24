namespace DAL.Configurations.SchemaPromotion
{
	public class CouponUsageConfiguration :IEntityTypeConfiguration<CouponUsage>
	{
		public void Configure(EntityTypeBuilder<CouponUsage> builder)
		{
			builder.ToTable("CouponUsages", "Promotions");

			builder.HasKey(cu => cu.Id);

			builder.HasOne(cu => cu.Coupon)
				   .WithMany(c => c.Usages)
				   .HasForeignKey(cu => cu.CouponId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(cu => cu.User)
				   .WithMany()
				   .HasForeignKey(cu => cu.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(cu => cu.Order)
				   .WithMany()
				   .HasForeignKey(cu => cu.OrderId)
				   .OnDelete(DeleteBehavior.Restrict);
			builder.HasQueryFilter(oi => !oi.IsDeleted);
		}
	}
}
