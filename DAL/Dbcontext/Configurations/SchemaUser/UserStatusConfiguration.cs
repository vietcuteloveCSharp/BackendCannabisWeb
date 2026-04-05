namespace DAL.Dbcontext.Configurations.SchemaUser
{
	public class UserStatusConfiguration :IEntityTypeConfiguration<UserStatus>
	{
		public void Configure(EntityTypeBuilder<UserStatus> builder)
		{
			// 1. Table & Schema
			builder.ToTable("UserStatuses", "Users");

			// 2. Primary Key
			builder.HasKey(us => us.Id);

			builder.Property(us => us.Id)
				.ValueGeneratedOnAdd();

			// 3. Properties Mapping
			builder.Property(us => us.Code)
				.IsRequired()
				.HasMaxLength(20); // Ví dụ: ACTIVE, BANNED, PENDING

			builder.Property(us => us.Name)
				.IsRequired()
				.HasMaxLength(50); // Ví dụ: Hoạt động, Bị khóa, Chờ xác nhận

			// 4. Relationships
			// Một Status có thể áp dụng cho nhiều User
			builder.HasMany(us => us.Users)
				.WithOne(u => u.Status)
				.HasForeignKey(u => u.StatusId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_USER_USERSTATUS_STATUSID");

			// 5. Indexes
			// Code thường dùng để query trong code (Enum mapping) nên cần Unique
			builder.HasIndex(us => us.Code)
				.IsUnique();
		}
	}
}
