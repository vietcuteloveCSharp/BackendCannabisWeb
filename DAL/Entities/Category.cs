namespace DAL.Entities
{
    public class Category :BaseEntity
    {
        [Key]    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100,ErrorMessage = "Category name not more than 100 characters.")]
        public string CategoryName { get; set; } = string.Empty;
		public string? Description { get; set; }

		//navigation
		public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
		public virtual ICollection<PromotionCategory> PromotionCategories { get; set; } = new HashSet<PromotionCategory>();

    }
}
