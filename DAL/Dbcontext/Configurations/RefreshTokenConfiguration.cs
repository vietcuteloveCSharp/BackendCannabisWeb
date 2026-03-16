using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.RefreshTokenValue).IsRequired().HasMaxLength(256);
			builder.Property(r => r.ExpiresAt).IsRequired();
			builder.Property(r => r.IsRevoked).IsRequired().HasDefaultValue(false);
			builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

			builder.HasOne(r => r.User)
				   .WithMany(c => c.RefreshTokens)
				   .HasForeignKey(r => r.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
