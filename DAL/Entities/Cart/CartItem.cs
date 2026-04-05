
namespace DAL.Entities.Cart
{
    public class CartItem :BaseEntity ,ISoftDelete
    {
     
        public int Id { get; set; }
        public int CartId {  get; set; }
		public int ProductVariantId { get; set; } // FK ProductVariant
  
        public int? Quantity { get; set; } = 1;

        public virtual ProductVariant ProductVariant { get; set; } = default!;
        public virtual Cart Cart { get; set; } = default!;
		public bool IsDeleted { get; set ; }
		public DateTime? DeletedAt { get ; set ; }
		public int? DeletedBy { get; set; }
	}
}
