namespace DAL.Entities
{
    public class Brand :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
        public string? Website {  get; set; }
		public bool IsPremium { get; set; } // Để ưu tiên hiển thị các hãng lớn
		public virtual ICollection<Nutrient> Nutrients { get;set; } = new HashSet<Nutrient>();
		public virtual ICollection<CarbonFilter> CarbonFilters { get; set; } = new List<CarbonFilter>();
        public virtual ICollection<GrowTent> GrowTents { get; set; } = new HashSet<GrowTent>();
        public virtual ICollection<Dehumidifier> Dehumidifiers { get; set; } = new HashSet<Dehumidifier>();
        public virtual ICollection<GrowLight> GrowLights { get; set; } = new HashSet<GrowLight>();
        public virtual ICollection<Product> Products { get; set; }= new HashSet<Product>();


    }
}
