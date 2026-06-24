namespace DAL.Configurations.SchemaShip
{
	public class ShipmentStatusConfiguration : IEntityTypeConfiguration<ShipmentStatus>
	{
		public void Configure(EntityTypeBuilder<ShipmentStatus> builder)
		{
			builder.ToTable("ShipmentStatuses","Ship");
			builder.HasKey(ss => ss.Id);

			builder.Property(ss => ss.Name)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(ss => ss.Description)
				   .HasMaxLength(200);
		}
	
	}
}
