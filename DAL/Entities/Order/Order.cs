using DAL.Entities.Inherited;
using DAL.Entities.Ship;

namespace DAL.Entities.Order
{
    public class Order : BaseEntity , ISoftDelete
    {
        public int  Id { get; set; }
        public int BuyerId { get; set; }

        public int? StaffId {  get; set; }
		public int StatusId {  get; set; }
        public decimal TotalAmount {  get; set; } // tổng tiền
        public string? ShippingAddress {  get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		//navigation
		public virtual User.User Buyer { get; set; } =default!;
		public virtual User.User Staff { get; set; } = default!;
        
        public virtual Payment.Payment? Payment { get; set; }
        public virtual OrderStatus OrderStatus { get; set; } = default!;
        public virtual ICollection<Shipment> Shipments { get; set; } =  new HashSet<Shipment>();
		public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new HashSet<OrderHistory>();
        public virtual ICollection< Review.Review> Reviews { get; set; } = new HashSet<Review.Review>();

    }
}
