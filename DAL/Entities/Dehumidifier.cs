namespace DAL.Entities
{
    public class Dehumidifier :BaseEntity
    {
        [Key]
        public int DehumidifierId { get; set; }
		public int ProductId { get; set; }
        public decimal DehumidificationCapacity { get; set; } // Capacity in liters/day or similar
        public decimal TankCapacityLiters { get; set; }
        public bool HasContinuousDrainage { get; set; }
		public bool HasAutoHumidistat { get; set; }
		public int Quantity { get; set; }
        [Required(ErrorMessage ="Id brand is required.")]
        public int BrandId { get; set; } // Foreign Key
       
        public decimal CoverageArea { get; set; } // In square meters or feet
       
        public decimal NoiseLevel { get; set; } // In dB
        
        public decimal PowerConsumption { get; set; } // In Watts
        public string Description { get; set; } = string.Empty;

		public virtual Brand Brand { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;    
    }
}
