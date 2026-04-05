using DAL.Entities.Product;

namespace DAL.Entities.Order
{
    public class OrderItem :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public int ProductVariantId {  get; set; }
        public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public virtual Order Order { get; set; } = default!;
        public virtual ProductVariant ProductVariant { get; set; } = default!;
    }
}
