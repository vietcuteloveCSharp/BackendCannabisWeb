namespace DAL.Configurations.SchemaShop
{
	public class CartConfiguration: IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable("Carts", "Shop");

			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();

			builder.Property(c => c.Session_Id).HasMaxLength(255); 
            builder.Property(c => c.Price).HasColumnType("decimal(18,2)"); 

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(c => c.IsDeleted).HasDefaultValue(false); 

            // Mỗi Customer chỉ được phép sở hữu một Giỏ hàng duy nhất (1-1)
            builder.HasIndex(c => c.CustomerId).IsUnique(); 

            builder.HasOne(c => c.Customer)
				.WithOne(cust => cust.Cart)
				.HasForeignKey<Cart>(c => c.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_Carts_Customers_CustomerId");

		}
	}
}
