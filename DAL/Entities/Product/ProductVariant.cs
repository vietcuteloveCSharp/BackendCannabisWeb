
using Shared.Common.Inherited;

namespace DAL.Entities.Product

{
	public class ProductVariant : BaseEntity,ISoftDelete
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string SKU { get; set; } = default!; // Mã SKU của variant
		public decimal Price { get; set; } // Giá
		public string? Barcode { get; set; } // Mã vạch (nếu cần)
											 // Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		// Navigation
		public Product Product { get; set; } = default!;
		public virtual ICollection<ProductVariantAttribute> Attributes { get; set; } = new HashSet<ProductVariantAttribute>();
		public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
		public virtual ICollection< Inventory.Inventory> Inventories { get; set; } = new List<Inventory.Inventory>();
		public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

	}
}
