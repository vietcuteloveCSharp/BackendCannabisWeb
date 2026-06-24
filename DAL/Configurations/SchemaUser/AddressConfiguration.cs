namespace DAL.Configurations.SchemaUser
{
	public class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses", "Users");
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id)
				  .ValueGeneratedOnAdd();

			builder.Property(a => a.UserId).IsRequired();
			builder.HasOne(a => a.User)
				  .WithMany(u => u.Addresses)
				  .HasForeignKey(a => a.UserId)
				  .OnDelete(DeleteBehavior.Cascade)
				  .HasConstraintName("FK_ADDRESS_USER_USERID");

			builder.Property(a => a.Country).HasMaxLength(150).IsRequired();
			builder.Property(a => a.City).HasMaxLength(100).IsRequired();
			builder.Property(a => a.Street).HasMaxLength(100).IsRequired();
			builder.Property(a => a.HouseNumber).HasMaxLength(20).IsRequired();
			builder.Property(a=>a.Ward).HasMaxLength(100).IsRequired();
			builder.Property(a => a.IsDefault).HasDefaultValue(false);
			builder.Property(a => a.IsDeleted).HasDefaultValue(false);
			// index
			builder.HasIndex(a => a.UserId);
		}
	}
}
