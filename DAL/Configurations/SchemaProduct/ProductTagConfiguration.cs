namespace DAL.Configurations.SchemaProduct
{
	public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
	{
		public void Configure(EntityTypeBuilder<ProductTag> builder)
		{
			builder.ToTable("ProductTags","Products");

			// Composite key
			builder.HasKey(pt => new { pt.ProductId, pt.TagId });

			// Navigation
			builder.HasOne(pt => pt.Product)
				.WithMany(p => p.ProductTags)
				.HasForeignKey(pt => pt.ProductId);

			builder.HasOne(pt => pt.Tag)
				.WithMany(t => t.ProductTags)
				.HasForeignKey(pt => pt.TagId);
		}
	}
	
}

