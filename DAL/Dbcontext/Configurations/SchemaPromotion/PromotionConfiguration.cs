

namespace DAL.Dbcontext.Configurations.SchemaPromotion
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
		}
	}
}
