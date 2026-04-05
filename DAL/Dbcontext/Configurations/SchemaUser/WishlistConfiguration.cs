
using DAL.Entities.Review;

namespace DAL.Dbcontext.Configurations.SchemaUser
{
	public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
		{
			builder.ToTable("Wishlists");

			builder.HasKey(w => w.Id);

			builder.HasOne(w => w.User)
			   .WithMany(u => u.Wishlists)
			   .HasForeignKey(w => w.UserId)
			   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(w => w.Product)
			   .WithMany(p => p.Wishlists)
			   .HasForeignKey(w => w.ProductId)
			   .OnDelete(DeleteBehavior.Cascade);
			builder.HasQueryFilter(oi => !oi.IsDeleted);
		}
	}

}
