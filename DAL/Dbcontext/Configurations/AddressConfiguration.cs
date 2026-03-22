using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses", "Users");
			builder.HasKey(a => a.AddressId);
			builder.Property(a => a.AddressId)
				  .ValueGeneratedOnAdd();

			builder.Property(a => a.UserId).IsRequired();
			builder.HasOne(a => a.User)
				  .WithMany(u => u.Addresses)
				  .HasForeignKey(a => a.UserId)
				  .OnDelete(DeleteBehavior.Cascade)
				  .HasConstraintName("FK_ADDRESS_USER_USERID");

			builder.Property(a => a.Country).HasMaxLength(150).IsRequired();
			builder.Property(a => a.Province).HasMaxLength(150).IsRequired();
			builder.Property(a => a.District).HasMaxLength(150).IsRequired();
			builder.Property(a => a.Commune).HasMaxLength(150).IsRequired();
			builder.Property(a => a.Road_Village_Hamlet).HasMaxLength(150).IsRequired();
			builder.Property(a => a.HouseNumber).HasMaxLength(20).IsRequired();
			builder.Property(a => a.PostalCode).HasMaxLength(30).IsRequired();
			builder.Property(a => a.IsDefault).HasDefaultValue(false);
		}
	}
}
