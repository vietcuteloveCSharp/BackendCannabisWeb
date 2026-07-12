using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext
{
	public class AuditDbContext :DbContext
	{	
		
		public AuditDbContext(DbContextOptions<AuditDbContext> options):base(options) 
		{
			
		}
		public DbSet<AuditLog> AuditLog { get; set; }
		public DbSet<EntityChange> EntityChange { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Cấu hình bảng AuditLog
			modelBuilder.Entity<AuditLog>(entity =>
			{
				entity.ToTable("AuditLogs", "Audit");
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Action).IsRequired().HasMaxLength(50);
				entity.Property(e => e.TableName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.ActionTime).IsRequired();

				// Quan hệ 1-Nhiều nội bộ giữa AuditLog và EntityChanges
				entity.HasMany(a => a.EntityChanges)
					  .WithOne(c => c.AuditLog)
					  .HasForeignKey(c => c.AuditLogId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// Cấu hình bảng EntityChange
			modelBuilder.Entity<EntityChange>(entity =>
			{
				entity.ToTable("EntityChanges", "dbo");
				entity.HasKey(e => e.Id);

				entity.Property(e => e.PropertyName).IsRequired().HasMaxLength(150);
			});
		}
	}
}
