namespace DAL.Dbcontext.Configurations.SchemaProduct
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

			builder.Property(v => v.Price)
				.HasColumnType("decimal(18,2)");

			builder.Property(v => v.Stock)
				.HasDefaultValue(0);

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
