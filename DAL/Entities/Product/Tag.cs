using Shared.Common.Inherited;

namespace DAL.Entities.Product
{
	public class Tag : BaseEntity ,ISoftDelete
	{
		public int Id { get; set; } // Khóa chính
		public string Name { get; set; } = default!;
		public string? Description { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get ; set ; }

		// Navigation: Nhiều product có thể có nhiều tag → mapping qua ProductTag
		public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();

	}
}
