namespace DAL.Entities
{
    public class Promotion :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string PromotionName { get; set; } = string.Empty;
		public string? Description { get; set; }
        public EDiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public int MinimumQuantity { get; set; } = 0;
        public decimal MaximumDiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } =false;

        //navigation
        public virtual ICollection<PromotionCategory> PromotionCategories {  get; set; } =new HashSet<PromotionCategory>();
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } = new HashSet<PromotionProduct>();
    }
}
