

using DAL.Entities.Inherited;

namespace DAL.Entities.Promotion
{

    public class CouponUsage : BaseEntity ,ISoftDelete
    {
       
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int UserId { get; set; }

        public int OrderId { get; set; } // nếu áp dụng cho 1 đơn hàng

		public bool IsDeleted { get ; set ; }
		public DateTime? DeletedAt { get; set ; }
		public int? DeletedBy { get; set; }
        // Navigation
        public Coupon Coupon { get; set; } = default!;
        public User.User User { get; set; } = default!;
        public Order.Order Order { get; set; } = default!;
	}
}
