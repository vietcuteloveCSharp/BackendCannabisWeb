using DAL.Entities.Inherited;

namespace DAL.Entities.Ship
{
	public class Shipment : BaseEntity, ISoftDelete
	{
		public int Id { get; set; } // Khóa chính
		public int OrderId { get; set; } // FK Order
		public int StatusId { get; set; } // FK ShipmentStatu
		public int MethodId { get; set; } // FK ShippingMethod

		public string? TrackingNumber { get; set; } // Mã vận đơn
		public decimal ShippingFee { get; set; } // Phí vận chuyển
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public Shop.Order Order { get; set; } = default!;
		public ShipmentStatus Status { get; set; } = default!;
		public ShippingMethod Method { get; set; } = default!;
		public ICollection<ShipmentItem> Items { get; set; } = new HashSet<ShipmentItem>();
	}
}
