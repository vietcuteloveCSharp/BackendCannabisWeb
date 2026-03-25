namespace DAL.Entities
{
    public class Seed :BaseEntity
    {
		[Key]
		public int SeedId { get; set; }
		public int ProductId { get; set; }
		public int BreederId { get; set; }

		// Chuyển sang decimal để Web làm được thanh trượt (Slider) Filter
		public decimal THCContent { get; set; }
		public decimal CBDContent { get; set; }

		public EStrainType StrainType { get; set; }
		public int ClassifyId { get; set; }
		public int FloweringTimeDays { get; set; }

		public decimal Yield { get; set; }
		public EDifficulty Difficulty { get; set; }
		public decimal Price { get; set; }

		// Thêm chiều cao để người dùng chọn lều phù hợp
		public int? IndoorHeightCm { get; set; }
		public string Genetics { get; set; } = string.Empty;

		public decimal IndicaPercentage { get; set; }
		public decimal SativaPercentage { get; set; }
		public int TotalQuantity { get; set; }
		public string Description { get; set; } = string.Empty;
		//navi
		public virtual Breeder Breeder { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
        public virtual Classification Classification { get; set; } = default!;

    }
}
