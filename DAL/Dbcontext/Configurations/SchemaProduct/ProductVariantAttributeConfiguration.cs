namespace DAL.Dbcontext.Configurations.SchemaProduct
{
	internal class ProductVariantAttributeConfiguration : IEntityTypeConfiguration<ProductVariantAttribute>
	{
		public void Configure(EntityTypeBuilder<ProductVariantAttribute> builder)
		{
			builder.ToTable("ProductVariantAttributes","Products");

			// Composite key
			builder.HasKey(v => new { v.ProductVariant, v.AttributeValueId });

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
