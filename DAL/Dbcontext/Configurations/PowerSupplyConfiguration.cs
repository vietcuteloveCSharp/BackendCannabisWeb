using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class PowerSupplyConfiguration : IEntityTypeConfiguration<PowerSupply>
	{
		public void Configure(EntityTypeBuilder<PowerSupply> builder)
		{
			builder.HasKey(c => c.PowerSupplyId);
			builder.Property(c => c.PowerSupplyId).ValueGeneratedOnAdd();
			builder.Property(c => c.Type).HasConversion<string>().IsRequired();
			builder.Property<int>(c => c.Voltage).IsRequired();
		}
	}
}
