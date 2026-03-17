namespace DAL.Entities
{
    public class PromotionProduct
    {
        [Key]
        public int PromotionId { get; set; }
        [Key]
        public int ProductId { get; set; }

        public Promotion Promotion { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
