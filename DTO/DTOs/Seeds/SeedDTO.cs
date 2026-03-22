namespace DTO.DTOs.Seeds
{
	public class SeedDTO
	{
		public int SeedId { get; set; }

		// --- THÔNG TIN LIÊN KẾT PRODUCT ---
		public int ProductId { get; set; } // Phải có để biết nó thuộc Product nào
		public string ProductName { get; set; } = string.Empty; // Để hiển thị tên SP
		public string? CategoryName { get; set; } // Cho biết nó thuộc loại hạt gì (Auto, Photo...)

		// --- THÔNG SỐ KỸ THUẬT ---
		public int BreederId { get; set; }
		public string? BreederName { get; set; } // Thêm cái này để hiển thị tên nhà lai tạo

		public string THCContent { get; set; } = string.Empty;
		public string CBDContent { get; set; } = string.Empty;

		public EStrainType StrainType { get; set; } // Indica/Sativa/Hybrid

		public int ClassifyId { get; set; }
		public string? ClassifyName { get; set; } // Thêm tên phân loại (Feminized, Regular...)

		public int FloweringTimeDays { get; set; }
		public decimal Yield { get; set; }
		public EDifficulty Difficulty { get; set; }

		// Lưu ý: Price và Quantity thường lấy từ bảng Product hoặc bảng Batch (lô hàng)
		// Nếu bạn để ở đây cũng được nhưng phải thống nhất logic với Product
		public decimal Price { get; set; }
		public int TotalQuantity { get; set; }

		public decimal IndicaPercentage { get; set; }
		public decimal SativaPercentage { get; set; }

		// --- TRẠNG THÁI & THỜI GIAN ---
		public bool IsActive { get; set; } // Trạng thái kinh doanh
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}
