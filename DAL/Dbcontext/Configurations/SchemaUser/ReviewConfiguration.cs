using DAL.Entities.Review;

namespace DAL.Dbcontext.Configurations.SchemaUser
{
	public class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.ToTable("Reviews");

			builder.HasKey(r => r.Id);
			builder.Property(c => c.ReviewTitle).HasMaxLength(100);
			builder.Property(c => c.Rating).IsRequired();
			builder.Property(c => c.Comments).HasMaxLength(400);
			builder.HasOne(r => r.Product)
				   .WithMany(p => p.Reviews)
				   .HasForeignKey(r => r.ProductId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(r => r.User)
				   .WithMany(u => u.Reviews)
				   .HasForeignKey(r => r.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasQueryFilter(r => !r.IsDeleted);
		}
	}
}


