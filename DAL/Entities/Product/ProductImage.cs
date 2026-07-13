using Shared.Common.Inherited;

namespace DAL.Entities.Product
{
    public class ProductImage : BaseEntity,ISoftDelete
    {
        [Key]
        public int Id { get; set; }
		[Required(ErrorMessage = "Id product is required.")]
		public int ProductId { get; set; } // Foreign Key
		public int? ProductVariantId { get; set; } // int? cho phép NULL nếu là ảnh chung của sản phẩm
		public string ImageUrl { get; set; } = string.Empty;  //link Cloudinary
		public string? AltText { get; set; } // Văn bản thay thế cho SEO

		public bool IsMainImage { get; set; } = false;
		// Soft delete
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		// Navigation Property
		public virtual Product? Product { get; set; }
		public virtual ProductVariant? ProductVariant { get; set; }
	}
}
