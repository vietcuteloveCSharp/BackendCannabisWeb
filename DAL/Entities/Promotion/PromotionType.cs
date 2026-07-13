
namespace DAL.Entities.Promotion
{
	public class PromotionType :BaseEntity
	{
		public int Id { get; set; }
		public string Code { get; set; } = default!; // Ví dụ: PERCENTAGE, FIXED_AMOUNT, BUY_1_GET_1, SAME_PRICE
		public string Name { get; set; } = default!; // Ví dụ: Giảm theo %, Giảm tiền mặt, Mua 1 tặng 1, Đồng giá
		public string? Description { get; set; }
		public virtual ICollection<Promotion> Promotions { get; set; } = new HashSet<Promotion>();
	}
}
