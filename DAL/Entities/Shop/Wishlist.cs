using DAL.Entities.Inherited;

namespace DAL.Entities.Shop
{
	public class Wishlist :BaseEntity ,ISoftDelete
	{
		public int Id { get; set; }
		public int CustomerId { get; set; } // FK User

		public int ProductId { get; set; } // FK Product
		public bool IsDeleted { get ; set ; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		// Navigation
		public Customer Customer{ get; set; } = default!;
		public Product.Product Product { get; set; } = default!;
	}
}
