using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
	{
		public void Configure(EntityTypeBuilder<AuditLog> builder)
		{
			builder.ToTable("AuditLogs", "Users");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.TableName)
				  .HasMaxLength(150)
				  .IsRequired();

			builder.Property(e => e.RecordId)
				  .HasMaxLength(100)
				  .IsRequired();

			builder.Property(e => e.Action)
				  .HasConversion<string>()   // enum -> string
				  .HasMaxLength(20)
				  .IsRequired();

			builder.Property(e => e.ColumnName)
				  .HasMaxLength(150);

			builder.Property(e => e.OldValue)
				  .HasColumnType("nvarchar(max)");
			builder.Property(e => e.NewValue)
				  .HasColumnType("nvarchar(max)");

			builder.Property(e => e.Description)
				 .HasMaxLength(1000);

			builder.Property(e => e.CreatedAt)
				  .HasDefaultValueSql("GETUTCDATE()"); // default từ SQL server

			// nếu muốn join sang Users
			builder.HasOne(e => e.User)
				  .WithMany(e => e.AuditLogs)
				  .HasForeignKey(e => e.UserId)
				  .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(e => e.Role)
				.WithMany(r => r.AuditLogs)
				.HasForeignKey(e => e.RoleId)
				 .OnDelete(DeleteBehavior.SetNull)
				.HasConstraintName("FK_AuditLog_Role_RoleId");
			builder.Property(e => e.RoleName).HasMaxLength(100).IsRequired(false);
			builder.HasIndex(e => e.TableName);
			builder.HasIndex(e => e.Action);
			builder.HasIndex(e => e.CreatedAt);
		}
	}
}
