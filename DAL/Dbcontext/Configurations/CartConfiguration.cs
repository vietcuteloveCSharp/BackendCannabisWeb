using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.HasKey(c => c.CartId);

			builder.Property(c => c.UserId)
				  .IsRequired();

			builder.Property(c => c.Session_Id)
				  .HasMaxLength(255)
				  .IsRequired();

			builder.Property(c => c.Price)
				  .HasColumnType("decimal(10,2)")
				  .IsRequired();

			builder.Property(c => c.Status)
				  .HasConversion<string>()
				  .HasMaxLength(20)
				  .IsRequired();

			builder.ToTable("Carts", "Orders", t =>
			{
				t.HasCheckConstraint("CK_Carts_UserOrSession",
					"(UserId IS NOT NULL AND Session_Id IS NULL) OR (UserId IS NULL AND Session_Id IS NOT NULL)");
			});
			// ✅ Unique index: chỉ một giỏ hàng active / user
			builder.HasIndex(e => e.UserId)
				.HasDatabaseName("UX_Cart_User")
				.IsUnique()
				.HasFilter("[Status] = 'Active' AND [UserId] IS NOT NULL");

			// ✅ Unique index: chỉ một giỏ hàng active / session
			builder.HasIndex(e => e.Session_Id)
				.HasDatabaseName("UX_Cart_Session")
				.IsUnique()
				.HasFilter("[Status] = 'Active' AND [Session_Id] IS NOT NULL");

		}
	}
}
