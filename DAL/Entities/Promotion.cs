namespace DAL.Entities
{
    [Table("Promotions", Schema = "Promotions")]
    public class Promotion :BaseEntity
    {
        [Key]
        public int PromotionId { get; set; }
        [Required(ErrorMessage = "Promotion name is required.")]
        [StringLength(150, ErrorMessage = "Promotion name no more than 150 characters.")]
        public string PromotionName { get; set; } = string.Empty;
		public string? Description { get; set; }
        [Required(ErrorMessage = "Discount type is required")]
        public EDiscountType DiscountType { get; set; }
        [Column(TypeName ="decimal(12,2)")]
        public decimal DiscountValue { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal MinimumOrderValue { get; set; }
        public int MinimumQuantity { get; set; } = 0;
        [Column(TypeName = "decimal(12,2)")]
        public decimal MaximumDiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } =false;

        //navigation
        public virtual ICollection<PromotionCategory> PromotionCategories {  get; set; } =new List<PromotionCategory>();
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } = new List<PromotionProduct>();
    }
}
