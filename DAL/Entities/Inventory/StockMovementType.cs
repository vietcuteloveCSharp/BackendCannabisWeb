namespace DAL.Entities.Inventory
{
	public class StockMovementType
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!; // Ví dụ: Giảm theo %, Giảm tiền mặt, Mua 1 tặng 1, Đồng giá
		public string? Description { get; set; }
		public virtual ICollection<StockMovement> StockMovements { get; set; } = new HashSet<StockMovement>();
	}
}
