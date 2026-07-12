namespace DAL.Configurations.Noti
{
	public class CustomerNotificationLogConfiguration : IEntityTypeConfiguration<CustomerNotificationLog>
	{
		public void Configure(EntityTypeBuilder<CustomerNotificationLog> builder)
		{
			builder.ToTable("CustomerNotificationLogs", "Noti");

			builder.HasKey(cnl => cnl.Id);
			builder.Property(cnl => cnl.Id).ValueGeneratedOnAdd();

			builder.Property(cnl => cnl.IsRead).HasDefaultValue(false); 
            builder.Property(cnl => cnl.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

			builder.HasIndex(cnl => cnl.NotificationId);
			builder.HasIndex(cnl => cnl.CustomerId); 

            // Xóa thông báo gốc thì xóa luôn log của Customer
            builder.HasOne(cnl => cnl.Notification)
				.WithMany(n => n.CustomerNotificationLogs)
				.HasForeignKey(cnl => cnl.NotificationId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_CustomerNotificationLogs_Notifications_NotificationId");

			// Xóa tài khoản Customer thì dọn sạch log thông báo của họ
			builder.HasOne(cnl => cnl.Customer)
				.WithMany(c => c.NotificationLogs)
				.HasForeignKey(cnl => cnl.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_CustomerNotificationLogs_Customers_CustomerId");
			// Giúp ẩn các bản ghi log nếu Customer sở hữu nó đã bị xóa mềm (!IsDeleted)
			builder.HasQueryFilter(cnl => !cnl.Customer.IsDeleted);
		}
	}
}