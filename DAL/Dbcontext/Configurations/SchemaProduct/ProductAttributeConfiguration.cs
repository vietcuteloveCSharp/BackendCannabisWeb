namespace DAL.Dbcontext.Configurations.SchemaProduct
{
	internal class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
	{
		public void Configure(EntityTypeBuilder<ProductAttribute> builder)
		{
			builder.ToTable("ProductAttributes","Products");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(a => a.Description)
				.HasMaxLength(500);

			builder.HasQueryFilter(a => !a.IsDeleted);

			// Navigation
			builder.HasMany(a => a.Values)
				.WithOne(v => v.Attribute)
				.HasForeignKey(v => v.AttributeId);
		}
	}
}
