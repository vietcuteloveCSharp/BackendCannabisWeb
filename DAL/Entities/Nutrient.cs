namespace DAL.Entities
{
    public class Nutrient :BaseEntity
    {
        [Key]
        public int Id { get; set; }
		public int ProductId { get; set; }
        public int BrandId {  get; set; }
        public int NutrientTypeId { get; set; }

		public EApplicationStage ApplicationStage { get; set; } // Giai đoạn sử dụng
		public bool IsPhBuffered { get; set; } // Có tự cân bằng pH không (Ví dụ dòng pH Perfect của Advanced Nutrients)
		public string DilutionRate { get; set; } = string.Empty; // Tỉ lệ pha khuyến cáo (ví dụ: 2ml/L)

		public int Quantity { get; set; }
        public decimal Price {  get; set; }
        public int VolumeMl { get; set; }
       
        public string Ingredients { get; set; } = string.Empty;
        public string NpkRatio { get; set; } = string.Empty; // Tỷ lệ NPK
        public bool IsOrganic { get; set; } = false;

        public string? Description { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string StorageInstructions { get; set; } = string.Empty;
        public virtual Brand Brand { get; set; } = default!;
        public virtual NutrientType NutrientType { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
         
    }
}
