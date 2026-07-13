
using Shared.Common.Inherited;

namespace DAL.Entities.Ship
{
	public class ShipmentItem :BaseEntity, ISoftDelete
	{
	
		public int Id { get; set; } // Khóa chính
	
		public int ShipmentId { get; set; } // FK shipment
		public int OrderItemId { get; set; } // FK orderitem
		public int Quantity { get; set; } = 1; // Số lượng item
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public Shipment Shipment { get; set; } = default!;
		public OrderItem OrderItem { get; set; } = default!;
	}

}

