using DAL.Entities.Cart;

namespace DAL.Dbcontext.Configurations.SchemaUser
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users", "Users");
			builder.HasKey(u => u.Id);

			builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
			builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
			builder.Property(u => u.Email).IsRequired();
			builder.Property(u=>u.PhoneNumber).IsRequired().HasMaxLength(30);
			builder.Property(u=>u.AvatarUrl).HasMaxLength(500);
			
			//User -role (N-1)
			builder.Property(u => u.RoleId).IsRequired();
			builder.HasOne(c => c.Role)
				   .WithMany(c => c.Users)
				   .HasForeignKey(c => c.RoleId)
				   .HasConstraintName("FK_USER_ROLE_ROLEID")
				   .OnDelete(DeleteBehavior.Restrict);
			

			// User - UserStatus (N - 1)
			builder.Property(c=>c.StatusId).IsRequired();
			builder.HasOne(u => u.Status)
				.WithMany(c => c.Users)
				.HasForeignKey(u => u.StatusId)
				.OnDelete(DeleteBehavior.Restrict);

			// User - Cart (1 - 1)
			builder.HasOne(u => u.Cart)
				.WithOne(c => c.User)
				.HasForeignKey<Cart>(c => c.UserId);

			// User - Orders (1 - N) - Cần phân biệt Buyer và Seller
			builder.HasMany(u => u.OrdersAsBuyer)
				.WithOne(o => o.Buyer) 
				.HasForeignKey(o => o.BuyerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(u => u.OrdersAsSeller)
				.WithOne(o => o.Seller) 
				.HasForeignKey(o => o.SellerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(u => u.Username).IsUnique();
			builder.HasIndex(u => u.Email).IsUnique();
		}
	}
}
