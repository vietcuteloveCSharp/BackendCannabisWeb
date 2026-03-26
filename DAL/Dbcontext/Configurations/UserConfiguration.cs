using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users", "Users");
			builder.HasKey(u => u.Id);
			builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
			builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
			builder.Property(u => u.Email).IsRequired();
			builder.Property(u => u.Status).HasConversion<string>()
			.HasMaxLength(20).HasDefaultValue(EUserStatus.Active);
			builder.Property(u => u.RoleId).IsRequired();

			builder.HasOne(c => c.Role)
				   .WithMany(c => c.Users)
				   .HasForeignKey(c => c.RoleId)
				   .HasConstraintName("FK_USER_ROLE_ROLEID")
				   .OnDelete(DeleteBehavior.Restrict)
					.IsRequired();

			builder.HasIndex(u => u.Username).IsUnique();
			builder.HasIndex(u => u.Email).IsUnique();
		}
	}
}
