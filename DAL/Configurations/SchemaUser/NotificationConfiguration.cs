namespace DAL.Configurations.SchemaUser
{
	public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.ToTable("Notifications");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Title).HasMaxLength(100);
			builder.Property(x => x.Message).HasMaxLength(300);
		}
	}
}
