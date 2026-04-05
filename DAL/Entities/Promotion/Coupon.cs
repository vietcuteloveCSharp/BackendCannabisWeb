namespace DAL.Entities.Promotion
{
	public class Coupon : BaseEntity, ISoftDelete
	{
		
		public int Id { get; set; }

		public string Code { get; set; } = default!;

		public int PromotionId { get; set; } // FK Promotion
		public decimal DiscountAmount { get; set; }
		public decimal? MinOrderAmount { get; set; }
		public DateTime? ExpiredAt { get; set; }

		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public Promotion Promotion { get; set; } = default!;
		public ICollection<CouponUsage> Usages { get; set; } = new HashSet<CouponUsage>();
	}
}
