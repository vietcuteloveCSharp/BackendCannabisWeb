using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(c => c.RoleId);
			builder.Property(c => c.RoleId).ValueGeneratedOnAdd();
			builder.Property(c => c.RoleName).IsRequired().HasConversion<string>();
			builder.Property(c => c.Description).HasMaxLength(255);
		}
	}
}
