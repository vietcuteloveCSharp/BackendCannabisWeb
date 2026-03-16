using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class ChipModelConfiguration : IEntityTypeConfiguration<ChipModel>
	{
		public void Configure(EntityTypeBuilder<ChipModel> builder)
		{
			builder.HasKey(c => c.ChipModelId);

			builder.Property(c => c.Manufacturer)
				  .HasMaxLength(100)
				  .IsRequired();

			builder.Property(c => c.ModelChip)
				  .HasMaxLength(100)
				  .IsRequired();

			builder.Property(c => c.Generation)
				  .HasMaxLength(50);

			builder.Property(c => c.Efficiency)
				  .HasColumnType("decimal(5,2)")
				  .IsRequired();

			builder.Property(c => c.Description)
				  .HasMaxLength(1000);

			// ✅ Index hữu ích khi search/filter
			builder.HasIndex(c => c.ModelChip)
				  .HasDatabaseName("IX_ChipModels_ModelChip");
		}
	}
}
