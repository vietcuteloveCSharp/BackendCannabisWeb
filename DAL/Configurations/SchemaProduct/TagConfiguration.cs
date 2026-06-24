namespace DAL.Configurations.SchemaProduct
{
	public class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.ToTable("Tags","Products");

			builder.HasKey(t => t.Id);

			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.Description)
				.HasMaxLength(500);

			// Soft delete filter
			builder.HasQueryFilter(t => !t.IsDeleted);

			// Navigation
			builder.HasMany(t => t.ProductTags)
				.WithOne(pt => pt.Tag)
				.HasForeignKey(pt => pt.TagId);
		}
	}
}
