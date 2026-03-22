namespace DAL.Entities
{
    public class PromotionCategory
    {
        [Key]
        public int PromotionId { get; set; }
        [Key]
        public int CategoryId { get;set; }

        public virtual Promotion Promotion { get; set; } = default!;
        public virtual Category Category { get; set; } = default!;

    }
}
