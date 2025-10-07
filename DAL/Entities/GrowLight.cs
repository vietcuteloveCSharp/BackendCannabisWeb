namespace DAL.Entities
{
    [Table("GrowLights", Schema = "Inventory")]
    public class GrowLight : BaseEntity
    {
        [Key]
        public int GrowLightId {  get; set; }
		[Required(ErrorMessage = "Id product is required.")]
		public int ProductId { get; set; }
		[Required(ErrorMessage = "Id brand is required.")]
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public int Wattage {  get; set; }
        [Column(TypeName = "decimal(10,2")]
        public decimal Price { get; set; }
        public int CoverageArea { get; set; } // Unit: m²
        public int WarrantyPeriod { get; set; } // Unit: months
        [Required(ErrorMessage = "Id powerSupply is required.")]
        public int PowerSupplyId { get; set; }
        [Required(ErrorMessage = "Id chipmodel is required.")]
        public int ChipModelId { get; set; }
        [Required(ErrorMessage = "Id cooling system is required.")]
        public int CoolingSystemId { get; set; }
        [Required(ErrorMessage = "Id spectrum is required.")]
        public int SpectrumId { get; set; }
        public int Lifespan { get; set; } // Unit: hours
        public string ModelNumber { get; set; } = string.Empty; // Số model
        public string Description { get; set; } = string.Empty;

		// Navigation Properties
		public virtual Brand? Brand { get; set; }
        public virtual PowerSupply? PowerSupply { get; set; }
        public virtual ChipModel? ChipModel { get; set; }
        public virtual CoolingSystem? CoolingSystem { get; set; }
        public virtual Spectrum? Spectrum { get; set; }
        public virtual Product? Product { get; set; }

    }
}
