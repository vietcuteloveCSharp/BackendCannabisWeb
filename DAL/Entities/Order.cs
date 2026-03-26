namespace DAL.Entities
{
    public class Order : BaseEntity
    {
        [Key]
        public int  Id { get; set; }
        public int BuyerId { get; set; }

        public int SellerId {  get; set; }
        public EOrderStatus OrderStatus {  get; set; }
        public decimal TotalAmount {  get; set; }
        public string ShippingAddress {  get; set; } = string.Empty;
        public decimal ShippingFee {  get; set; }
        public string TrackingNumber {  get; set; } =string.Empty;
        //navigation
        public virtual User Buyer { get; set; } =default!;
		public virtual User Seller { get; set; } = default!;
		public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual Payment? Payment { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
