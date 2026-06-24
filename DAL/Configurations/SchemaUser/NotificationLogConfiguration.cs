namespace DAL.Configurations.SchemaUser
{
	public  class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLog>
	{
		public void Configure(EntityTypeBuilder<NotificationLog> builder)
		{
			builder.ToTable("NotificationLogs");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.UserId);
			builder.Property(x => x.NotificationId);
			builder.Property(x => x.IsRead).HasDefaultValue(false);

			builder.HasQueryFilter(nl => !nl.Notification.IsDeleted);

			builder.HasIndex(x => x.UserId).HasDatabaseName("IX_UserId");
			builder.HasIndex(x => x.NotificationId).HasDatabaseName("IX_NotificationId");
		}
	}
}
