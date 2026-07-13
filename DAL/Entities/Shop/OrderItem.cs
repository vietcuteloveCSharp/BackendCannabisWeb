using Shared.Common.Inherited;
using DAL.Entities.Product;

namespace DAL.Entities.Shop
{
    public class OrderItem :BaseEntity,ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public int ProductVariantId {  get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductNameSnapshot { get; set; } = default!;
		public string? VariantNameSnapshot { get; set; } = default!;
		public bool IsDeleted { get; set ; }
		public DateTime? DeletedAt { get ; set ; }
		public int? DeletedBy { get ; set ; }
		public virtual Order Order { get; set; } = default!;
        public virtual ProductVariant ProductVariant { get; set; } = default!;

    }
}
