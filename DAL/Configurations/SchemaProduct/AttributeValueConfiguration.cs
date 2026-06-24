namespace DAL.Configurations.SchemaProduct
{
	public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
	{
		public void Configure(EntityTypeBuilder<AttributeValue> builder)
		{
			builder.ToTable("AttributeValues","Products");

			builder.HasKey(v => v.Id);

			builder.Property(v => v.Value)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasQueryFilter(v => !v.IsDeleted);

			// FK Attribute
			builder.HasOne(v => v.Attribute)
				.WithMany(a => a.Values)
				.HasForeignKey(v => v.AttributeId);
		}
	}
}
