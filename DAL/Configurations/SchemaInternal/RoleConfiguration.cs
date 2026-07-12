namespace DAL.Configurations.SchemaInternal
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles", "Internal");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.RoleName).IsRequired().HasMaxLength(30);
			builder.Property(c => c.Description).HasMaxLength(255);
			builder.HasIndex(r => r.RoleName).IsUnique();
		}
	}
}
