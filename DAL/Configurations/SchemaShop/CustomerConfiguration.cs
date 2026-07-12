namespace DAL.Configurations.SchemaShop
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable("Customers", "Shop");

			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();

			builder.Property(c => c.Username)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(c => c.PasswordHash)
				.IsRequired()
				.HasMaxLength(255);

			builder.Property(c => c.Email)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(c => c.Name)
				.HasMaxLength(150);

			builder.Property(c => c.PhoneNumber)
				.HasMaxLength(20);

			builder.Property(c => c.AvatarUrl)
				.HasMaxLength(500);

			builder.Property(c => c.IsActive)
				.HasDefaultValue(true);

			// BaseEntity defaults
			builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(c => c.IsDeleted).HasDefaultValue(false);

			// Chỉ mục tối ưu hóa tìm kiếm tài khoản khách hàng
			builder.HasIndex(c => c.Username).IsUnique();
			builder.HasIndex(c => c.Email).IsUnique();
		}
	}
}
