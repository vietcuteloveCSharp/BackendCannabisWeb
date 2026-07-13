namespace DAL.Configurations.SchemaPromotion
{
	public class PromotionConfiguration :IEntityTypeConfiguration<Promotion>
	{
		public void Configure(EntityTypeBuilder<Promotion> builder)
		{
			builder.ToTable("Promotions", "Promotions");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Name)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(p => p.Description)
				   .HasMaxLength(500);

			builder.HasQueryFilter(p => !p.IsDeleted);
			// Thay thế đoạn cấu hình int cũ bằng khóa ngoại FK độc lập
			builder.HasOne(p => p.PromotionType)
				.WithMany(t => t.Promotions)
				.HasForeignKey(p => p.TypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Promotions_PromotionTypes_TypeId");

			builder.Property(p => p.DiscountValue)
				.IsRequired()
				.HasPrecision(18, 2); // Đặt độ chính xác cho giá trị giảm giá
		}
	}
}
