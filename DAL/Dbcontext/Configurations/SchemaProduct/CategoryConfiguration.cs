namespace DAL.Dbcontext.Configurations.SchemaProduct
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			// 1. Table & Schema
			builder.ToTable("Categories", "Products");

			// 2. Primary Key
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();

			// 3. Properties Mapping
			builder.Property(c => c.CategoryName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(c => c.Description)
				.HasMaxLength(500);


			// 4. Relationships

			// --- CẤU HÌNH ĐỆ QUY (Parent - Children) ---
			builder.HasOne(c => c.Parent)
				.WithMany(c => c.Children)
				.HasForeignKey(c => c.ParentId)
				.OnDelete(DeleteBehavior.Restrict) // Tránh xóa cha làm lỗi con
				.HasConstraintName("FK_CATEGORY_CATEGORY_PARENTID");

			// Category - Products (1 - N)
			builder.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_PRODUCT_CATEGORY_CATEGORYID");

			// 5. Indexes
			builder.HasIndex(c => c.CategoryName)
				.IsUnique()
				.HasDatabaseName("IX_Categories_CategoryName");

			builder.HasIndex(c => c.ParentId);
		}
	}
}


