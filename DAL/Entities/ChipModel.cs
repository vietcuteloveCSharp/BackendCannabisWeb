namespace DAL.Entities
{
    [Table("ChipModels",Schema = "Inventory")]
    public class ChipModel : BaseEntity
    {
        [Key]
        public int ChipModelId { get; set; }
        [StringLength(100, ErrorMessage = "Manufacturer no more than 100 characters.")]
        public string Manufacturer { get; set; } = string.Empty; // Nhà sản xuất (ví dụ: "Samsung", "Osram")
        [StringLength(100, ErrorMessage = "ModelChip no more than 100 characters.")]
        public string ModelChip { get; set; } = string.Empty; // Tên model chip (ví dụ: "301H", "301L")
        [StringLength(50, ErrorMessage = "Generation no more than 50 characters.")]
        public string? Generation { get; set; } // Thế hệ (nếu có, ví dụ: "Gen 2", "Gen 3")
        [Column(TypeName ="decimal(5,2)")]
        public decimal Efficiency { get; set; } // Hiệu suất (lumen trên watt, tùy chọn)
        public string Description { get; set; } = string.Empty;
		// Điều hướng quan hệ (Navigation Property)
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new List<GrowLight>(); // Một model chip có thể dùng trong nhiều thiết bị chiếu sáng
    }
}
