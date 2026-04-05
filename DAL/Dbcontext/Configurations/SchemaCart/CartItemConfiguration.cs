
namespace DAL.Dbcontext.Configurations.SchemaCart
{
	public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
	{
		public void Configure(EntityTypeBuilder<CartItem> builder)
		{
			builder.ToTable("CartItems", "Cart");

			builder.HasKey(ci => ci.Id);

			builder.HasOne(ci => ci.Cart)
				   .WithMany(c => c.CartItems)
				   .HasForeignKey(ci => ci.CartId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(ci => ci.ProductVariant)
				   .WithMany()
				   .HasForeignKey(ci => ci.ProductVariantId)
				   .OnDelete(DeleteBehavior.Restrict);
			builder.HasIndex(cd => cd.CartId)
				  .HasDatabaseName("IX_CartDetails_CartId");

			builder.HasIndex(cd => cd.ProductVariantId)
				.HasDatabaseName("IX_CartDetails_ProductVariantId");
		}
	}
}
