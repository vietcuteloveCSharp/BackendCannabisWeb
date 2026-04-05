namespace DAL.Entities.Promotion
{
    public class PromotionCategory
    {
        
        public int PromotionId { get; set; }
        
        public int CategoryId { get;set; }

        public virtual Promotion Promotion { get; set; } = default!;
        public virtual Category Category { get; set; } = default!;

    }
}
