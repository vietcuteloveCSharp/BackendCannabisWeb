namespace DAL.Configurations.SchemaUser
{
	public class UserSessionConfiguration :IEntityTypeConfiguration<UserSession>
	{
		public void Configure(EntityTypeBuilder<UserSession> builder)
		{
			// 1. Table & Schema
			builder.ToTable("UserSessions", "Users");

			// 2. Primary Key
			builder.HasKey(us => us.Id);
			builder.Property(us => us.Id)
				.ValueGeneratedOnAdd();

			// 3. Properties Mapping
			// AccessToken thường là JWT nên có độ dài lớn, dùng nvarchar(max) hoặc giới hạn cao
			builder.Property(us => us.SessionId)
				.IsRequired().HasMaxLength(50);

			builder.Property(us => us.ExpiredAt)
				.IsRequired();

			builder.Property(us => us.Device)
				.HasMaxLength(255);

			builder.Property(us => us.IpAddress)
				.HasMaxLength(50);

			// BaseEntity properties (Giả định có CreatedAt từ BaseEntity)
			builder.Property(us => us.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// 4. Relationships
			// Một User có thể có nhiều Session (đăng nhập trên nhiều thiết bị)
			builder.HasOne(us => us.User)
				.WithMany(u => u.Sessions)
				.HasForeignKey(us => us.UserId)
				.OnDelete(DeleteBehavior.Cascade) // Nếu xóa User thì xóa sạch Session
				.HasConstraintName("FK_USERSESSION_USER_USERID");

			// 5. Indexes
			// Index trên UserId để truy vấn danh sách session của 1 user (ví dụ: để đăng xuất từ xa)
			builder.HasIndex(us => us.UserId);
			builder.HasIndex(us => us.SessionId);
		}
	}
}
