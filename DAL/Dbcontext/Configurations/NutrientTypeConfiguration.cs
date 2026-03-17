using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class NutrientTypeConfiguration : IEntityTypeConfiguration<NutrientType>
	{
		public void Configure(EntityTypeBuilder<NutrientType> builder)
		{
			builder.ToTable("NutrientTypes", "Inventory");
			builder.HasKey(c => c.NutrientTypeId);
			builder.Property(c => c.NutrientName).HasMaxLength(150).IsRequired();
			builder.Property(c => c.Description).HasMaxLength(1000);
		}
	}
}
