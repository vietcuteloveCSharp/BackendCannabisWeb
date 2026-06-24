
using DAL.Entities.Inherited;

namespace DAL.Entities.Order
{
	public class OrderStatus : BaseEntity
	{
		public int Id { get; set; } // Khóa chính
		public string Name { get; set; } = default!; // Ví dụ: Pending, Paid, Shipped, Delivered

		public string? Description { get; set; }

		// Navigation
		public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
	}
}
