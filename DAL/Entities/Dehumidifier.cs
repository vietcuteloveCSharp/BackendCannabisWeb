namespace DAL.Entities
{
    [Table("Dehumidifiers", Schema = "Inventory")]
    public class Dehumidifier :BaseEntity
    {
        [Key]
        public int DehumidifierId { get; set; }
		[Required(ErrorMessage = "Id product is required.")]
		public int ProductId { get; set; }
		[Column(TypeName ="decimal(3,2")] 
        public decimal DehumidificationCapacity { get; set; } // Capacity in liters/day or similar
        public int Quantity { get; set; }
        [Required(ErrorMessage ="Id brand is required.")]
        public int BrandId { get; set; } // Foreign Key
        [Column(TypeName = "decimal(10,2")]
        public decimal CoverageArea { get; set; } // In square meters or feet
        [Column(TypeName = "decimal(5,2")]
        public decimal NoiseLevel { get; set; } // In dB
        [Column(TypeName = "decimal(10,2")]
        public decimal PowerConsumption { get; set; } // In Watts
        public string Description { get; set; } = string.Empty;

		public virtual Brand? Brand { get; set; }
        public virtual Product? Product { get; set; }    
    }
}
