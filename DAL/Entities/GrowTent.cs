namespace DAL.Entities
{
    public class GrowTent :BaseEntity
    {
        [Key]
        public int GrowtentId { get; set; }
		[Required(ErrorMessage = "Id product is required.")]
		public int ProductId { get; set; }
		[Required(ErrorMessage ="Id brand is required.")]
        public int BrandId { get; set; }
        [StringLength(100,ErrorMessage = "Dimensions no more than 100 characters.")]
        public string Dimensions { get; set; } = string.Empty;
		[StringLength(255, ErrorMessage = "Material no more than 255 characters.")]

        public string Material { get; set; }  =string.Empty;
		public bool Waterproof { get; set; }=false;
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
        [StringLength(255, ErrorMessage = "Frame material no more than 255 characters.")]
        public string FrameMaterial { get; set; } = string.Empty;
		public int WarrantyPeriod { get; set; }
        public string? Description { get; set; }

        //navigation

        public virtual Brand? Brand { get; set; }
        public virtual Product? Product { get; set; }

    }
}
