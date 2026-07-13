using Shared.Common.Inherited;

namespace DAL.Entities.Product
{
    public class Category :BaseEntity
    {
        [Key]    
        
        public int Id { get; set; }
      
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
		public int? ParentId { get; set; } // FK self-reference

		//navigation
		public Category? Parent { get; set; }
		public virtual ICollection<Category> Children { get; set; } = new HashSet<Category>();
		public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public virtual ICollection<PromotionCategory> Promotions { get; set; } = new HashSet<PromotionCategory>();
	}
}
