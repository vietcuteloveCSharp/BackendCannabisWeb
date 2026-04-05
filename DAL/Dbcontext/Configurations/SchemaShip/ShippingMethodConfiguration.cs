
namespace DAL.Dbcontext.Configurations.SchemaShip
{
	public class ShippingMethodConfiguration:IEntityTypeConfiguration<ShippingMethod>
    {
		public void Configure(EntityTypeBuilder<ShippingMethod> builder)
		{
			builder.ToTable("ShippingMethods", "Ship");
			builder.HasKey(sm => sm.Id);

			builder.Property(sm => sm.Name)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(sm => sm.Description)
				   .HasMaxLength(200);
		}
	}
}

