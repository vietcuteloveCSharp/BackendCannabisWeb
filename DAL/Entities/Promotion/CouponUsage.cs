
namespace DAL.Entities.Promotion
{

    public class CouponUsage : BaseEntity
    {
       
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int UserId { get; set; }

        public int OrderId { get; set; } // nếu áp dụng cho 1 đơn hàng

        // Navigation
        public Coupon Coupon { get; set; } = default!;
        public User.User User { get; set; } = default!;
        public Order.Order Order { get; set; } = default!;
	}
}
