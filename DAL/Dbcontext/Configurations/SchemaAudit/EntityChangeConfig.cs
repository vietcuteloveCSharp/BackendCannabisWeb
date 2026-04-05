
namespace DAL.Dbcontext.Configurations.SchemaAudit
{
	public class EntityChangeConfig :IEntityTypeConfiguration<EntityChange>
	{
		public void Configure(EntityTypeBuilder<EntityChange> builder)
		{
			builder.ToTable("EntityChanges");

			builder.HasKey(ec => ec.Id);
			builder.Property(c => c.PropertyName);
			builder.Property(c => c.NewValue);
			builder.Property(c => c.OldValue);
			builder.HasOne(ec => ec.AuditLog)
				   .WithMany(a => a.EntityChanges)
				   .HasForeignKey(ec => ec.AuditLogId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
