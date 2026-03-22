namespace DAL.Entities
{
    public class Order : BaseEntity
    {
        [Key]
        public int  OrderId { get; set; }
        [Required(ErrorMessage ="Id customer is required.")]
        public int BuyerId { get; set; }
        [Required(ErrorMessage = "Id seller is required.")]

        public int SellerId {  get; set; }
        [Column(TypeName ="nvarchar(20)")]
        public EOrderStatus OrderStatus {  get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public decimal TotalAmount {  get; set; }
        public string ShippingAddress {  get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal ShippingFee {  get; set; }
        [Column(TypeName = "varchar(50)")]
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
