using Shared.Common.Inherited;

namespace DAL.Entities.Shop
{
	public class OrderHistory :   BaseEntity
	{
	
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int StatusId { get; set; }

		public string? Note { get; set; }

		// Navigation
		public Order Order { get; set; } = default!;
		public OrderStatus Status { get; set; } = default!;
	}
}
