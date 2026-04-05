namespace DAL.Dbcontext.Configurations.SchemaProduct
{
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.ToTable("Brands", "Products");
			builder.HasKey(b => b.Id);

			builder.Property(b => b.BrandName)
				  .HasMaxLength(255)
				  .IsRequired();

			builder.Property(b => b.Country)
				  .HasMaxLength(150);

			builder.Property(b => b.Description)
				  .HasMaxLength(1000);

			builder.Property(b => b.Website)
				  .HasMaxLength(255);

			builder.Property(b => b.IsPremium).HasDefaultValue(false);

			// ✅ Index: BrandName (search nhiều) 
			builder.HasIndex(b => b.BrandName)
				  .IsUnique()
				  .HasDatabaseName("IX_Brands_BrandName");
		}
	}
}
