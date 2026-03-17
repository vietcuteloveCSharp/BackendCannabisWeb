namespace DAL.Entities
{
    public class Brand :BaseEntity
    {
        [Key]
        public int BrandId { get; set; }
        [Required(ErrorMessage ="Brand name is required.")]
        [StringLength(255,ErrorMessage = "Brand name cannot exceed 255 characters.")] 
        public string BrandName { get; set; } = string.Empty;
		[StringLength(150, ErrorMessage = "Country name cannot exceed 150 characters.")]
        public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
        [StringLength(255,ErrorMessage = "Website link cannot exceed 255 characters.")]
        public string? Website {  get; set; } 
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Nutrient> Nutrients { get;set; } = new HashSet<Nutrient>();
		public virtual ICollection<CarbonFilter> CarbonFilters { get; set; } = new List<CarbonFilter>();
        public virtual ICollection<GrowTent> GrowTents { get; set; } = new HashSet<GrowTent>();
        public virtual ICollection<Dehumidifier> Dehumidifiers { get; set; } = new HashSet<Dehumidifier>();
        public virtual ICollection<GrowLight> GrowLights { get; set; } = new HashSet<GrowLight>();
        public virtual ICollection<Product> Products { get; set; }= new HashSet<Product>();


    }
}
