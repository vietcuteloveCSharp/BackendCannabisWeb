//định nghĩa kiểu (Size, Color…)

using DAL.Entities.Inherited;

namespace DAL.Entities.Product
{
	public class ProductAttribute : BaseEntity, ISoftDelete
	{
		public int Id { get; set; } // Khóa chính

		public string Name { get; set; } = default!; // Tên thuộc tính (Color, Size…)

		public string? Description { get; set; } // Mô tả

		// Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public ICollection<AttributeValue> Values { get; set; } = new HashSet<AttributeValue>();
	}
}
