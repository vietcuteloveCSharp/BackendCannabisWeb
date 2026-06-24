namespace DAL.Configurations.SchemaUser
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles", "Users");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.RoleName).IsRequired().HasMaxLength(30);
			builder.Property(c => c.Description).HasMaxLength(255);
		}
	}
}
