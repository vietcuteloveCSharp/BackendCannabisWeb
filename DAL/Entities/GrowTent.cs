namespace DAL.Entities
{
    public class GrowTent :BaseEntity
    {
        [Key]
        public int Id { get; set; }
		public int ProductId { get; set; }
        public int BrandId { get; set; }
        public string Dimensions { get; set; } = string.Empty;
		public int WidthCm { get; set; }  // Rộng
		public int LengthCm { get; set; } // Dài
		public int HeightCm { get; set; } // Cao
        public string Material { get; set; } = string.Empty;
        public int? CanvasDensity { get; set; }
		public string ReflectiveMaterial { get; set; } =string.Empty;
		public bool Waterproof { get; set; }=false;
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
        public string FrameMaterial { get; set; } = string.Empty;
		public int WarrantyPeriod { get; set; }
        public string? Description { get; set; }

        //navigation

        public virtual Brand? Brand { get; set; }
        public virtual Product? Product { get; set; }

    }
}
