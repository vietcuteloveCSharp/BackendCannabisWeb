//định nghĩa kiểu (Size, Color…)
namespace DAL.Entities.Product
{
	public class AttributeValue : BaseEntity, ISoftDelete
	{
	
		public int Id { get; set; } // Khóa chính

		public int AttributeId { get; set; } // FK ProductAttribute

		public string Value { get; set; } = default!; // Giá trị (Red, XL…)

		// Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public ProductAttribute Attribute { get; set; } = default!;
		public ICollection<ProductVariantAttribute> VariantMappings { get; set; } = new HashSet<ProductVariantAttribute>();
	}
}
