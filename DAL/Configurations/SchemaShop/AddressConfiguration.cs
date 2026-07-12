namespace DAL.Configurations.SchemaShop
{
	public class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses","Shop");
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id)
				  .ValueGeneratedOnAdd();

			builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
			builder.Property(a => a.City).IsRequired().HasMaxLength(100);
			builder.Property(a => a.Street).IsRequired().HasMaxLength(150);
			builder.Property(a => a.Ward).IsRequired().HasMaxLength(100);
			builder.Property(a => a.HouseNumber).IsRequired().HasMaxLength(50);

			builder.Property(a => a.IsDefault).HasDefaultValue(false);
			builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(a => a.IsDeleted).HasDefaultValue(false);

			builder.HasIndex(a => a.CustomerId);

			// Mối quan hệ với Customer
			builder.HasOne(a => a.Customer)
				.WithMany(c => c.Addresses)
				.HasForeignKey(a => a.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Addresses_Customers_CustomerId");
			builder.HasIndex(a => a.CustomerId);
		}
	}
}
