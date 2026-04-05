namespace DAL.Entities.Review
{
	public class Wishlist :BaseEntity
	{
		public int Id { get; set; }
		public int UserId { get; set; } // FK User

		public int ProductId { get; set; } // FK Product
		// Navigation
		public User.User User { get; set; } = default!;
		public Product.Product Product { get; set; } = default!;
	}
}
