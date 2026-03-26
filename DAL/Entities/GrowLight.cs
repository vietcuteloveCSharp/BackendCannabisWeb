namespace DAL.Entities
{
    public class GrowLight : BaseEntity
    {
        [Key]
        public int Id {  get; set; }
		public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public int Wattage {  get; set; }
        public decimal Price { get; set; }
		public decimal? PPF { get; set; }      // umol/s
        public decimal? Efficacy { get; set; } // umol/j
		public bool IsDimmable { get; set; }
		public int CoverageArea { get; set; } // Unit: m²
        public int WarrantyPeriod { get; set; } // Unit: months
        public int PowerSupplyId { get; set; }
        public int ChipModelId { get; set; }
        public int CoolingSystemId { get; set; }
        public int SpectrumId { get; set; }
        public int Lifespan { get; set; } // Unit: hours
        public string ModelNumber { get; set; } = string.Empty; // Số model
        public string Description { get; set; } = string.Empty;

		// Navigation Properties
		public virtual Brand Brand { get; set; } = default!;
        public virtual PowerSupply PowerSupply { get; set; } = default!;
        public virtual ChipModel ChipModel { get; set; } = default!;
        public virtual CoolingSystem CoolingSystem { get; set; } = default!;
        public virtual Spectrum Spectrum { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;

    }
}
