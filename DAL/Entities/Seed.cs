namespace DAL.Entities
{
    public class Seed :BaseEntity
    {
        [Key]
        public int SeedId { get; set; }
		[Required(ErrorMessage = "Id product is required.")]
		public int ProductId { get; set; }
		[Required(ErrorMessage ="Id breeder is required.")]
        public int BreederId {  get; set; }
        [Column(TypeName ="varchar(30)")]
        public string THCContent { get; set; } = string.Empty;
		[Column(TypeName = "varchar(30)")]
        public string CBDContent {  get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(20)")]
        public EStrainType StrainType { get; set; }
        [Required(ErrorMessage = "Id Classify is required.")]
        public int ClassifyId { get; set; }
        public int FloweringTimeDays { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Yield {  get; set; } //sản lượng
        [Column(TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "Difficulty is required.")]
        public EDifficulty  Difficulty { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal IndicaPercentage {  get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal SativaPercentage { get; set; }
        public int TotalQuantity {  get; set; }
        public string Description { get; set; } =string.Empty;
		//navi
		public virtual Breeder Breeder { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
        public virtual Classification Classification { get; set; } = default!;

    }
}
