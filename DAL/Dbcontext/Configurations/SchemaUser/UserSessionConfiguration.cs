namespace DAL.Dbcontext.Configurations.SchemaUser
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
			builder.Property(us => us.AccessToken)
				.IsRequired();

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

			// Index trên AccessToken để tìm kiếm session cực nhanh khi filter request
			// Lưu ý: Nếu AccessToken quá dài (> 900 bytes), SQL Server sẽ không cho tạo Index trực tiếp.
			// Trong trường hợp đó, bạn có thể chỉ cần Index trên UserId.
			builder.HasIndex(us => us.AccessToken);
		}
	}
}
