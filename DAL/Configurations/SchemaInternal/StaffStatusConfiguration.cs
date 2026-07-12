namespace DAL.Configurations.SchemaInternal
{
	public class StaffStatusConfiguration :IEntityTypeConfiguration<StaffStatus>
	{
		public void Configure(EntityTypeBuilder<StaffStatus> builder)
		{
			// 1. Table & Schema

			builder.ToTable("StaffStatuses", "Internal");

			builder.HasKey(ss => ss.Id);
			builder.Property(ss => ss.Id).ValueGeneratedOnAdd();

			builder.Property(ss => ss.Code)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(ss => ss.Name)
				.IsRequired()
				.HasMaxLength(100);

			// Đảm bảo mã trạng thái là duy nhất (Ví dụ: "Active", "Banned")
			builder.HasIndex(ss => ss.Code).IsUnique();
		}
	}
}
