using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class CoolingSystemConfiguration : IEntityTypeConfiguration<CoolingSystem>
	{
		public void Configure(EntityTypeBuilder<CoolingSystem> builder)
		{
			builder.HasKey(c => c.CoolingSystemId);

			builder.Property(c => c.Type)
				  .HasConversion<string>()
				  .HasMaxLength(20)
				  .IsRequired();
			builder.Property(c => c.Description)
				  .HasMaxLength(1000);
		}
	}
}
