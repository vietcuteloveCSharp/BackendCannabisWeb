using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class BreederConfiguration : IEntityTypeConfiguration<Breeder>
	{
		public void Configure(EntityTypeBuilder<Breeder> builder)
		{
			builder.ToTable("Breeds", "Products");
			builder.HasKey(b => b.BreederId);

			builder.Property(b => b.BreederName)
				  .HasMaxLength(255)
				  .IsRequired();

			builder.Property(b => b.Country)
				  .HasMaxLength(150);

			builder.Property(b => b.Description)
				  .HasMaxLength(1000);

			builder.Property(b => b.Website)
				  .HasMaxLength(255);

			builder	.Property(b => b.IsActive)
				  .HasDefaultValue(true);

			builder.Property(b => b.Email)
				  .HasMaxLength(150)
				  .IsRequired();

			builder.HasIndex(b => b.Email)
				  .IsUnique()
				  .HasDatabaseName("IX_Breeder_Email");

			builder.Property(b => b.PhoneNumber)
				  .HasMaxLength(20);
		}
	}
}
