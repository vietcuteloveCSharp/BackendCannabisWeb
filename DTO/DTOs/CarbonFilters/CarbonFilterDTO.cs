namespace DTO.DTOs.CarbonFilters
{
	public class CarbonFilterDTO
	{    
		public int CarbonFilterId { get; set; }
		public string CarbonFilterName { get; set; } = string.Empty; // Tên bộ lọc
		public string AirflowRate { get; set; } = string.Empty; // Lưu lượng không khí
		public int BrandId { get; set; } // Mã thương hiệu
		public int Quantity { get; set; } = 0;
		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; } // Giá
		public string FilterMaterial { get; set; } = string.Empty; // Chất liệu lọc
		[Column(TypeName = "decimal(4,2)")]
		public decimal Diameter { get; set; } // Đường kính
		[Column(TypeName = "decimal(4,2)")]
		public decimal Length { get; set; } // Chiều dài
		public int Lifespan { get; set; } // Tuổi thọ (giờ hoặc ngày)
		[Column(TypeName = "decimal(3,2)")]
		public decimal MinTemperature { get; set; } // Nhiệt độ tối thiểu
		[Column(TypeName = "decimal(3,2)")]
		public decimal MaxTemperature { get; set; } // Nhiệt độ tối đa
		public string? Description { get; set; } // Mô tả sản phẩm
		public int WarrantyPeriod { get; set; }//thời gian bảo hành
		public string ModelNumber { get; set; } = string.Empty; // Số model
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; }
	}
}
