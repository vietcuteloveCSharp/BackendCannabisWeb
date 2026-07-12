namespace DAL.Configurations.Internal
{
	public class StaffRefreshTokenConfiguration : IEntityTypeConfiguration<StaffRefreshToken>
	{
		public void Configure(EntityTypeBuilder<StaffRefreshToken> builder)
		{
			builder.ToTable("StaffRefreshTokens", "Internal");

			builder.HasKey(srt => srt.Id);
			builder.Property(srt => srt.Id).ValueGeneratedOnAdd();

			builder.Property(srt => srt.TokenHash)
				.IsRequired()
				.HasMaxLength(500);

            builder.Property(srt => srt.Device).HasMaxLength(255); 
            builder.Property(srt => srt.IpAddress).HasMaxLength(50); 
            
            builder.Property(srt => srt.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(srt => srt.IsUsed).HasDefaultValue(false); 
            builder.Property(srt => srt.IsRevoked).HasDefaultValue(false); 
            builder.Property(srt => srt.IsDeleted).HasDefaultValue(false); 
            builder.HasIndex(srt => srt.TokenHash).IsUnique();

			// Cấu hình mối quan hệ tránh lỗi vòng lặp Cascade Paths của SQL Server
			builder.HasOne(srt => srt.Staff)
				.WithMany(s => s.RefreshTokens)
				.HasForeignKey(srt => srt.StaffId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_StaffRefreshTokens_Staffs_StaffId");

			builder.HasOne(srt => srt.StaffSession)
				.WithMany(ss => ss.RefreshTokens)
				.HasForeignKey(srt => srt.StaffSessionId)
				.OnDelete(DeleteBehavior.NoAction) // Sử dụng NoAction để chặn đụng độ luồng xóa của SQL
				.HasConstraintName("FK_StaffRefreshTokens_StaffSessions_StaffSessionId");
		}
	}
}