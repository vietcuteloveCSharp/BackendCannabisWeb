namespace DAL.Dbcontext.Configurations.SchemaProduct
{
	public class ProductVariantAttributeConfiguration : IEntityTypeConfiguration<ProductVariantAttribute>
	{
		public void Configure(EntityTypeBuilder<ProductVariantAttribute> builder)
		{
			builder.ToTable("ProductVariantAttributes","Products");

			// Composite key
			builder.HasKey(v => new { v.ProductVariantId, v.AttributeValueId });
			builder.HasQueryFilter(v => !v.ProductVariant.IsDeleted && !v.AttributeValue.IsDeleted);

			// Navigation
			builder.HasOne(v => v.ProductVariant)
				.WithMany(v => v.Attributes)
				.HasForeignKey(v => v.ProductVariantId);

			builder.HasOne(v => v.AttributeValue)
				.WithMany(a => a.VariantMappings)
				.HasForeignKey(v => v.AttributeValueId);
		}
	}
}
