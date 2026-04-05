namespace DAL.Entities.Ship
{
	public class ShipmentItem :BaseEntity
	{
	
		public int Id { get; set; } // Khóa chính
	
		public int ShipmentId { get; set; } // FK shipment
		public int OrderItemId { get; set; } // FK orderitem
		public int Quantity { get; set; } = 1; // Số lượng item

		// Navigation
		public Shipment Shipment { get; set; } = default!;
		public Order.OrderItem OrderItem { get; set; } = default!;
	}

}

