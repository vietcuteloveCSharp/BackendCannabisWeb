using DAL.Entities.Inherited;

namespace DAL.Entities.Product
{
    public class Brand :BaseEntity ,ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
        public string? Website {  get; set; }
        public bool IsPremium { get; set; } // Để ưu tiên hiển thị các hãng lớn
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		public virtual ICollection<Product> Products { get; set; }= new HashSet<Product>();
	
	}
}
