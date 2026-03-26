namespace DAL.Entities
{
    public class ProductImage : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Id product is required.")]
        public int ProductId { get; set; } // Foreign Key
        public string ImageUrl { get; set; } = string.Empty;  //link Cloudinary
        public bool IsMainImage { get; set; } = false;
        // Navigation Property
        public virtual Product? Product { get; set; }
    }
}
