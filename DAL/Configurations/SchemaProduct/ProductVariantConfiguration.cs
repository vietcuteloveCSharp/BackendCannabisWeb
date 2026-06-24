namespace DAL.Configurations.SchemaProduct
{
	public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
	{
		public void Configure(EntityTypeBuilder<ProductVariant> builder)
		{
			builder.ToTable("ProductVariants","Products");

			builder.HasKey(v => v.Id);

			builder.Property(v => v.SKU)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(v => v.SKU).IsUnique();

			builder.Property(v => v.Price)
				.HasPrecision(18,2);


			// FK Product
			builder.HasOne(v => v.Product)
				.WithMany(p => p.Variants)
				.HasForeignKey(v => v.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			// Soft delete filter
			builder.HasQueryFilter(v => !v.IsDeleted);
		}
	}
}
