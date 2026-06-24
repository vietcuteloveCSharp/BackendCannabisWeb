namespace DAL.Configurations.SchemaCart
{
	public class CartConfiguration: IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable("Carts","Cart");

			builder.HasKey(c => c.Id);

			builder.HasOne(c => c.User)
				   .WithOne(u => u.Cart)
				   .HasForeignKey<Cart>(c => c.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
			builder.Property(c => c.Price).HasPrecision(18, 2);
			builder.HasQueryFilter(c => !c.IsDeleted);
			// ✅ Unique index: chỉ một giỏ hàng active / user
			builder.HasIndex(e => e.UserId)
				.HasDatabaseName("UX_Cart_User")
				.IsUnique()
				.HasFilter("[IsDeleted] = 0");

			// ✅ Unique index: chỉ một giỏ hàng active / session
			builder.HasIndex(e => e.Session_Id)
				.HasDatabaseName("UX_Cart_Session")
				.IsUnique()
				.HasFilter("[IsDeleted] = 0 AND [Session_Id] IS NOT NULL");

		}
	}
}
