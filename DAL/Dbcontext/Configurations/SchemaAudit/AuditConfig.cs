

namespace DAL.Dbcontext.Configurations.SchemaAudit
{
	internal class AuditConfig : IEntityTypeConfiguration<AuditLog>
	{
		public void Configure(EntityTypeBuilder<AuditLog> builder)
		{
			builder.ToTable("AuditLogs", "Audit");

			builder.HasKey(a => a.Id);

			builder.HasOne(a => a.User)
				   .WithMany(u => u.AuditLogs)
				   .HasForeignKey(a => a.UserId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasQueryFilter(a => !a.IsDeleted);
		}
	}
}

