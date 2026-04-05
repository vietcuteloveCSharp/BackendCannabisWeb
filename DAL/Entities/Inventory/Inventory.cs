
namespace DAL.Entities.Inventory
{
	public class Inventory : BaseEntity, ISoftDelete
	{
		public int Id { get; set; } // Khóa chính

		public int ProductVariantId { get; set; } // FK ProductVariant

		public int Quantity { get; set; } // Số lượng hiện tại

		// Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public ProductVariant ProductVariant { get; set; } = default!;
		public ICollection<StockMovement> StockMovements { get; set; } = new HashSet<StockMovement>();
	}
}
