namespace DAL.Entities.Promotion
{
    public class PromotionProduct
    {
		[Key]
		public int PromotionId { get; set; }
		[Key]
		public int ProductId { get; set; }

		// Navigation
		public Promotion Promotion { get; set; } = default!;
		public Product.Product Product { get; set; } = default!;
	}
}
