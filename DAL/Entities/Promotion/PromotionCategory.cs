
namespace DAL.Entities.Promotion
{
    public class PromotionCategory :BaseEntity,ISoftDelete
    {
        
        public int PromotionId { get; set; }
        
        public int CategoryId { get;set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

        public virtual Promotion Promotion { get; set; } = default!;
        public virtual Category Category { get; set; } = default!;
	}
}
