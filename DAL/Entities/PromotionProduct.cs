namespace DAL.Entities
{
    [Table("Promotion_Produc", Schema ="Promotions")]
    public class PromotionProduct
    {
        [Key]
        public int PromotionId { get; set; }
        [Key]
        public int ProductId { get; set; }

        public Promotion? Promotion { get; set; }
        public Product? Product { get; set; }
    }
}
