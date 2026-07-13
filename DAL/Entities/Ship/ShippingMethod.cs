using Shared.Common.Inherited;

namespace DAL.Entities.Ship
{
	public class ShippingMethod :BaseEntity
	{
		
		public int Id { get; set; } // Khóa chính

		public string Name { get; set; } = default!; // Ví dụ: Standard, Express, COD

		public string? Description { get; set; }

		// Navigation
		public ICollection<Shipment> Shipments { get; set; } = new HashSet<Shipment>();
	}
}
