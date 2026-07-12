namespace DAL.Configurations.SchemaUser
{
	public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.ToTable("Notifications", "Noti");

			builder.HasKey(n => n.Id);
			builder.Property(n => n.Id).ValueGeneratedOnAdd();

			builder.Property(n => n.Title)
				.IsRequired()
				.HasMaxLength(255); 

            builder.Property(n => n.Message)
				.HasMaxLength(2000); 

            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(n => n.IsDeleted).HasDefaultValue(false);
		
		}
	}
}
