using Shared.Common.Inherited;

namespace DAL.Entities.Product
{
    public class Product :BaseEntity,ISoftDelete
    {
        
        [Key] 
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; } //fk category
        public int BrandId { get; set; } //fk brand
		public string? Description { get; set; }
		// Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		//naviagtion
		public virtual ICollection<Shop.Review> Reviews { get; set; } = new HashSet<Shop.Review>();
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
		public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } = new HashSet<PromotionProduct>();
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
		public ICollection<ProductVariant> Variants { get; set; } = new HashSet<ProductVariant>();
        public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
        public ICollection<Promotion.Promotion> promotions { get; set; }=new HashSet<Promotion.Promotion>();    
		public virtual Category Category { get; set; } = default!;
        public virtual Brand Brand { get; set; } = default!;
		
	}
}
