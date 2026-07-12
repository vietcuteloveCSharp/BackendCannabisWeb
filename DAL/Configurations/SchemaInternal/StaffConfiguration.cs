
namespace DAL.Configurations.SchemaInternal
{
	public class StaffConfiguration : IEntityTypeConfiguration<Staff>
	{
		public void Configure(EntityTypeBuilder<Staff> builder)
		{
			builder.ToTable("Staffs", "Internal");

			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd();

			builder.Property(s => s.StaffCode)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(s => s.Username)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.PasswordHash)
				.IsRequired()
				.HasMaxLength(255);

			builder.Property(s => s.Email)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(s => s.Name)
				.HasMaxLength(150);

			builder.Property(s => s.PhoneNumber)
				.HasMaxLength(20);


			builder.Property(s => s.AvatarUrl)
				.HasMaxLength(500);

			// BaseEntity & ISoftDelete defaults
			builder.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(s => s.IsDeleted).HasDefaultValue(false);

			// Indexes độc lập tăng tốc tìm kiếm tài khoản nhân viên
			builder.HasIndex(s => s.StaffCode).IsUnique();
			builder.HasIndex(s => s.Username).IsUnique();
			builder.HasIndex(s => s.Email).IsUnique();

			// Relationships
			builder.HasOne(s => s.Role)
				.WithMany(r => r.Staffs)
				.HasForeignKey(s => s.RoleId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Staffs_Roles_RoleId");

			builder.HasOne(s => s.Status)
				.WithMany(st => st.Staffs)
				.HasForeignKey(s => s.StatusId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Staffs_StaffStatuses_StatusId");
		}
	}
}
