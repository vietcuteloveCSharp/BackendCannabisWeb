
using DAL.Entities.Inherited;

namespace DAL.Entities.Review
{
	public class Wishlist :BaseEntity ,ISoftDelete
	{
		public int Id { get; set; }
		public int UserId { get; set; } // FK User

		public int ProductId { get; set; } // FK Product
		// Navigation
		public User.User User { get; set; } = default!;
		public Product.Product Product { get; set; } = default!;
		public bool IsDeleted { get ; set ; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
	}
}
