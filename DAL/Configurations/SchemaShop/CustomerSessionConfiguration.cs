namespace DAL.Configurations.SchemaShop
{
	public class CustomerSessionConfiguration : IEntityTypeConfiguration<CustomerSession>
	{
		public void Configure(EntityTypeBuilder<CustomerSession> builder)
		{
			builder.ToTable("CustomerSessions", "Shop");

			builder.HasKey(cs => cs.Id);
			builder.Property(cs => cs.Id).ValueGeneratedOnAdd();

			builder.Property(cs => cs.SessionToken)
				.IsRequired()
				.HasMaxLength(255);

			builder.Property(cs => cs.UserAgent).HasMaxLength(500);
			builder.Property(cs => cs.IpAddress).HasMaxLength(50);

			builder.Property(cs => cs.LoginAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(cs => cs.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(cs => cs.IsDeleted).HasDefaultValue(false);

			builder.HasIndex(cs => cs.SessionToken).IsUnique();
			builder.HasIndex(cs => cs.CustomerId);

			builder.HasOne(cs => cs.Customer)
				.WithMany(c => c.CustomerSessions)
				.HasForeignKey(cs => cs.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_CustomerSessions_Customers_CustomerId");
		}
	}
}
