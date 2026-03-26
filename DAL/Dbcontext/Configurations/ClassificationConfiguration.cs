using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class ClassificationConfiguration : IEntityTypeConfiguration<Classification>
	{
		public void Configure(EntityTypeBuilder<Classification> builder)
		{
			builder.ToTable("Classifications", "Products");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.Type).HasConversion<string>().IsRequired();

			builder.Property(c => c.Description)
				  .HasMaxLength(1000); // tránh nvarchar(max)

			builder.Property(c => c.IsActive)
				  .HasDefaultValue(true);

			
		}
	}
}
