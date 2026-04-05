namespace DAL.Dbcontext.Configurations.SchemaProduct
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products", "Products");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.ProductName).HasMaxLength(255).IsRequired();
			builder.Property(c =>c.Description).HasMaxLength(255);

			builder.HasOne(c => c.Category)
				   .WithMany(c => c.Products)
				   .HasForeignKey(c => c.CategoryId)
				   .HasConstraintName("FK_PRODUCT_CATEGORY_CATEGORYID")
				   .OnDelete(DeleteBehavior.Restrict)
				   .IsRequired();

			builder.HasOne(c => c.Brand)
				   .WithMany(c => c.Products)
				   .HasForeignKey(c => c.BrandId)
				   .HasConstraintName("FK_PRODUCT_BRAND_BRANDID")
				   .OnDelete(DeleteBehavior.Restrict)
				   .IsRequired();

			builder.HasIndex(c => c.ProductName).HasDatabaseName("IX_Product_ProductName").IsUnique();
		}
	}
}
