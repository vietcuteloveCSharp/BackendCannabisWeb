using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.HasKey(b => b.BrandId);

			builder.Property(b => b.BrandName)
				  .HasMaxLength(255)
				  .IsRequired();

			builder.Property(b => b.Country)
				  .HasMaxLength(150);

			builder.Property(b => b.Description)
				  .HasMaxLength(1000);

			builder.Property(b => b.Website)
				  .HasMaxLength(255);

			builder.Property(b => b.IsActive)
				  .HasDefaultValue(true);

			// ✅ Index: BrandName (search nhiều) 
			builder.HasIndex(b => b.BrandName)
				  .IsUnique()
				  .HasDatabaseName("IX_Brands_BrandName");
		}
	}
}
