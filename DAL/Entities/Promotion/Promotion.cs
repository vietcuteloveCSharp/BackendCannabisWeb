using DAL.Entities.Inherited;

namespace DAL.Entities.Promotion
{
	public class Promotion :BaseEntity,ISoftDelete
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string? Description { get; set; }
		public DateTime? StartAt { get; set; }
		public DateTime? EndAt { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public ICollection<PromotionProduct> Products { get; set; } = new HashSet<PromotionProduct>();
		public ICollection<PromotionCategory> Categories { get; set; } = new HashSet<PromotionCategory>();
		public ICollection<Coupon> Coupons { get; set; } = new HashSet<Coupon>();
	}
}
