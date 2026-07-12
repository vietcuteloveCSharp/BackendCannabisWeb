namespace DAL.Configurations.Internal
{
	public class StaffSessionConfiguration : IEntityTypeConfiguration<StaffSession>
	{
		public void Configure(EntityTypeBuilder<StaffSession> builder)
		{
			builder.ToTable("StaffSessions", "Internal");

			builder.HasKey(ss => ss.Id);
			builder.Property(ss => ss.Id).ValueGeneratedOnAdd();

			builder.Property(ss => ss.SessionToken)
				.IsRequired()
				.HasMaxLength(255); 

            builder.Property(ss => ss.UserAgent)
				.HasMaxLength(500); 

            builder.Property(ss => ss.IpAddress)
				.HasMaxLength(50);

            builder.Property(ss => ss.LoginAt).HasDefaultValueSql("GETUTCDATE()"); 
            builder.Property(ss => ss.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(ss => ss.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(ss => ss.SessionToken).IsUnique();
			builder.HasIndex(ss => ss.StaffId); 

            // Xóa tài khoản nhân viên thì dọn sạch toàn bộ phiên đăng nhập của người đó (Cascade)
            builder.HasOne(ss => ss.Staff)
				.WithMany(s => s.StaffSessions)
				.HasForeignKey(ss => ss.StaffId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_StaffSessions_Staffs_StaffId");
		}
	}
}