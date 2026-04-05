using static Enum.Domain.Product_Inventory;
namespace DAL.Entities.Inventory
{
	public class StockMovement : BaseEntity, ISoftDelete
	{
		public int Id { get; set; }

		public int InventoryId { get; set; } // FK Inventory

		public int QuantityChanged { get; set; } // Số lượng tăng/giảm

		public EStockMovementType MovementType { get; set; } // Loại di chuyển

		public string? Note { get; set; } // Ghi chú nếu cần

		// Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public Inventory Inventory { get; set; } = default!;
	}
}
