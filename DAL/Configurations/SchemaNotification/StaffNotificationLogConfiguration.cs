namespace DAL.Configurations.Noti
{
	public class StaffNotificationLogConfiguration : IEntityTypeConfiguration<StaffNotificationLog>
	{
		public void Configure(EntityTypeBuilder<StaffNotificationLog> builder)
		{
			builder.ToTable("StaffNotificationLogs", "Noti");

			builder.HasKey(snl => snl.Id);
			builder.Property(snl => snl.Id).ValueGeneratedOnAdd();

			builder.Property(snl => snl.IsRead).HasDefaultValue(false);
            builder.Property(snl => snl.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

			builder.HasIndex(snl => snl.NotificationId);
			builder.HasIndex(snl => snl.StaffId); 

            // Xóa thông báo gốc thì xóa luôn log của Staff
            builder.HasOne(snl => snl.Notification)
				.WithMany(n => n.StaffNotificationLogs)
				.HasForeignKey(snl => snl.NotificationId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_StaffNotificationLogs_Notifications_NotificationId");

			// Xóa tài khoản Staff thì dọn sạch log thông báo của họ
			builder.HasOne(snl => snl.Staff)
				.WithMany(s => s.NotificationLogs)
				.HasForeignKey(snl => snl.StaffId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_StaffNotificationLogs_Staffs_StaffId");
			// Giúp ẩn các bản ghi log nếu Notification gốc của nó đã bị xóa mềm (!IsDeleted)
			builder.HasQueryFilter(snl => !snl.Notification.IsDeleted);
		}
	}
}