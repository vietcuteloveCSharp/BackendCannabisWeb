//map giữa pro->tap
namespace DAL.Entities.Product
{
	public class ProductTag
	{
		public int ProductId { get; set; } // FK Product
		public int TagId { get; set; } // FK Tag

		// Navigation
		public Product Product { get; set; } = default!;
		public Tag Tag { get; set; } = default!;
	}
}
