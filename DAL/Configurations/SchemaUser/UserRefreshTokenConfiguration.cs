namespace DAL.Configurations.SchemaUser
{
	public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
	{
		public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
		{
			builder.ToTable("RefreshTokens", "Users");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();

			builder.Property(c => c.TokenHash).IsRequired().HasMaxLength(512);
			builder.Property(r => r.ExpiresAt).IsRequired();
			builder.Property(r => r.IsRevoked).IsRequired().HasDefaultValue(false);
			builder.Property(rt => rt.IsUsed).HasDefaultValue(false);
			builder.Property(rt => rt.Device).HasMaxLength(255);
			builder.Property(rt => rt.IpAddress).HasMaxLength(50);
			builder.HasOne(r => r.User)
				   .WithMany(c => c.RefreshTokens)
				   .HasForeignKey(r => r.UserId)
				   .OnDelete(DeleteBehavior.Cascade)
				   .HasConstraintName("FK_REFRESHTOKEN_USER_USERID");
			builder.HasOne(r => r.UserSession)
			   .WithMany(s => s.RefreshTokens) // Bạn nhớ 
			   .HasForeignKey(r => r.UserSessionId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(rt => rt.TokenHash)
				.IsUnique();
			builder.HasQueryFilter(rt => !rt.User.IsDeleted);
		}
	}
}
