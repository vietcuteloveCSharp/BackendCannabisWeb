namespace DAL.Entities.Promotion
{
    public class PromotionProduct
    {
		
		public int Id { get; set; }
		public int PromotionId { get; set; }

		public int ProductId { get; set; }

		// Navigation
		public Promotion Promotion { get; set; } = default!;
		public Product.Product Product { get; set; } = default!;
	}
}
