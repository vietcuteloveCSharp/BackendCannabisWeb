namespace DAL.Entities
{
    [Table("Promotion_Category", Schema = "Promotions")]
    public class PromotionCategory
    {
        [Key]
        public int PromotionId { get; set; }
        [Key]
        public int CategoryId { get;set; }

        public virtual Promotion? Promotion { get; set; }
        public  virtual Category? Category { get; set; }

    }
}
