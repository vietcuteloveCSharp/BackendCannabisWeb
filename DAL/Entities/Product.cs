namespace DAL.Entities
{
    public class Product :BaseEntity
    {
        
        [Key] 
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Product name is required.")]
        [StringLength(255, ErrorMessage = "Product name no more than 255 characters.")]
        public string ProductName { get; set; } = string.Empty;

		[Required(ErrorMessage ="Id category is required.")]
        public int CategoryId { get; set; } 
        public bool IsActive { get; set; } =true;
        public int BrandId { get; set; }
        public string? ProductType { get; set; }
        //naviagtion
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<CartDetails> CartsDetails { get; set; } = new HashSet<CartDetails>();
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
		public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } = new HashSet<PromotionProduct>();
		public virtual Category Category { get; set; } = default!;
        public virtual Seed? Seed { get; set; }
        public virtual Nutrient? Nutrient { get; set; }
        public virtual Dehumidifier? Dehumidifier { get; set; }
        public virtual GrowTent? GrowTent{ get;set; }
        public virtual GrowLight? GrowLight { get; set; }
        public virtual CarbonFilter? CarbonFilter { get; set; }
        public virtual Brand Brand { get; set; } = default!;
    }
}
