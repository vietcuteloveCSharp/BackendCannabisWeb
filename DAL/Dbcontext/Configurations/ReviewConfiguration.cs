using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.ToTable("Reviews", "Reviews");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();

			builder.HasOne(c => c.User)
				.WithMany(c => c.Reviews)
				.HasForeignKey(c => c.UserId)
				.HasConstraintName("FK_REVIEW_USER_USERID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasOne(c => c.Product)
				.WithMany(c => c.Reviews)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_REVIEW_PRODUCT_PRODUCTID")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasOne(c => c.Order)
				.WithMany(c => c.Reviews)
				.HasForeignKey(c => c.OrderId)
				.HasConstraintName("FK_REVIEW_ORDER_ORDERID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.Property(c => c.Rating)
				.HasColumnType("int")
				.IsRequired();

			builder.ToTable(tb => tb.HasCheckConstraint("CK_Review_Rating", "Rating BETWEEN 1 AND 5"));

			builder.Property(c => c.Comments)
				.HasMaxLength(2000);

			builder.Property(c => c.ReviewTitle)
				.HasMaxLength(255);
		}
	}
}
