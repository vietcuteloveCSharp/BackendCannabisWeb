using Microsoft.EntityFrameworkCore;

namespace DAL.Configurations.SchemaShop
{
	public class CustomerRefreshTokenConfiguration : IEntityTypeConfiguration<CustomerRefreshToken>
	{
		public void Configure(EntityTypeBuilder<CustomerRefreshToken> builder)
		{
			builder.ToTable("CustomerRefreshTokens", "Shop");

			builder.HasKey(crt => crt.Id);
			builder.Property(crt => crt.Id).ValueGeneratedOnAdd();

			builder.Property(crt => crt.TokenHash)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(crt => crt.Device).HasMaxLength(255);
			builder.Property(crt => crt.IpAddress).HasMaxLength(50);

			builder.Property(crt => crt.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.Property(crt => crt.IsUsed).HasDefaultValue(false);
			builder.Property(crt => crt.IsRevoked).HasDefaultValue(false);
			builder.Property(crt => crt.IsDeleted).HasDefaultValue(false);

			builder.HasIndex(crt => crt.TokenHash).IsUnique();

			builder.HasOne(crt => crt.Customer)
				.WithMany(c => c.RefreshTokens)
				.HasForeignKey(crt => crt.CustomerId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_CustomerRefreshTokens_Customers_CustomerId");

			builder.HasOne(crt => crt.CustomerSession)
				.WithMany(cs => cs.RefreshTokens)
				.HasForeignKey(crt => crt.CustomerSessionId)
				.OnDelete(DeleteBehavior.NoAction) // Không dùng Cascade ở đây để tránh Multiple Cascade Paths
				.HasConstraintName("FK_CustomerRefreshTokens_CustomerSessions_CustomerSessionId");
		}
	}
}
