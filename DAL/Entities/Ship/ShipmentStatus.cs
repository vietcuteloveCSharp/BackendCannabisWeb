using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Ship
{
	public class ShipmentStatus :BaseEntity
	{
		[Key]
		public int Id { get; set; } // Khóa chính

		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = default!; // Ví dụ: Pending, Shipped, Delivered

		public string? Description { get; set; }

		// Navigation
		public ICollection<Shipment> Shipments { get; set; } = new HashSet<Shipment>();
	}
}
