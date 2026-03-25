namespace DAL.Entities
{
    public class ChipModel : BaseEntity
    {
        [Key]
        public int ChipModelId { get; set; }
        public string Manufacturer { get; set; } = string.Empty; // Nhà sản xuất (ví dụ: "Samsung", "Osram")
		public string ModelName { get; set; } = string.Empty;
		public string ModelChip { get; set; } = string.Empty; // Tên model chip (ví dụ: "301H", "301L")
        public string? Generation { get; set; } // Thế hệ (nếu có, ví dụ: "Gen 2", "Gen 3")
        public decimal Efficiency { get; set; } // Hiệu suất (lumen trên watt, tùy chọn)
		
		public string Description { get; set; } = string.Empty;
		// Điều hướng quan hệ (Navigation Property)
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new HashSet<GrowLight>(); // Một model chip có thể dùng trong nhiều thiết bị chiếu sáng
    }
}
