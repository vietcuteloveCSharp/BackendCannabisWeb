using DAL.Entities.Inherited;
using DAL.Entities.Ship;

namespace DAL.Entities.Shop
{
    public class Order : BaseEntity , ISoftDelete
    {
        public int  Id { get; set; }
        public int CustomerId { get; set; }

        public int? StaffId {  get; set; }
		public int StatusId {  get; set; }
        public decimal TotalAmount {  get; set; } // tổng tiền
        public string? ShippingAddress {  get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		//navigation
		public virtual Customer Buyer { get; set; } = default!; 
		public virtual Staff? Staff { get; set; } = default!;   

		public virtual Payment? Payment { get; set; }
        public virtual OrderStatus OrderStatus { get; set; } = default!;
        public virtual ICollection<Shipment> Shipments { get; set; } =  new HashSet<Shipment>();
		public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new HashSet<OrderHistory>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
