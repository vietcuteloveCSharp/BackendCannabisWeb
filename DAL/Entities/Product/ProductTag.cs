//map giữa pro->tap

using Shared.Common.Inherited;

namespace DAL.Entities.Product
{
	public class ProductTag : BaseEntity,ISoftDelete
	{
		public int ProductId { get; set; } // FK Product
		public int TagId { get; set; } // FK Tag
		public bool IsDeleted { get; set ; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public Product Product { get; set; } = default!;
		public Tag Tag { get; set; } = default!;
	}
}
