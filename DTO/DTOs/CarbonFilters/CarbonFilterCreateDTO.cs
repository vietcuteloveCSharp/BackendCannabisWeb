namespace DTO.DTOs.CarbonFilters
{
	public class CarbonFilterCreateDTO 
	{
		[Required(ErrorMessage = "CarbonFilter name is required.")]
		[StringLength(255, ErrorMessage = "CarbonFilter name cannot exceed 255 characters.")]
		public string CarbonFilterName { get; set; } = string.Empty; // Tên bộ lọc
		[StringLength(150, ErrorMessage = "AirflowRate name cannot exceed 150 characters.")]
		public string AirflowRate   { get; set; } = string.Empty; // Lưu lượng không khí
		[Required(ErrorMessage = " Id Brand is required.")]
		public int BrandId { get; set; } // Mã thương hiệu
		[Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative.")]
		public int Quantity { get; set; } = 0;
		[Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; } // Giá
		[Required(ErrorMessage = "FilterMaterial is required.")]
		[StringLength(100, ErrorMessage = "FilterMaterial cannot exceed 100 characters.")]
		public string FilterMaterial { get; set; } = string.Empty; // Chất liệu lọc
		[Column(TypeName = "decimal(4,2)")]
		[Range(0, 9999.99, ErrorMessage = "Diameter must be greater than 0.")]
		public decimal Diameter { get; set; } // Đường kính
		[Column(TypeName = "decimal(4,2)")]
		[Range(0, 9999.99, ErrorMessage = "Length must be greater than 0.")]
		public decimal Length { get; set; } // Chiều dài
		[Range(1, int.MaxValue, ErrorMessage = "Lifespan must be at least 1.")]
		public int Lifespan { get; set; } // Tuổi thọ (giờ hoặc ngày)
		[Column(TypeName = "decimal(3,2)")]
		[Range(0, 100, ErrorMessage = "MinTemperature must be between 0 and 100.")]
		public decimal MinTemperature { get; set; } // Nhiệt độ tối thiểu
		[Column(TypeName = "decimal(3,2)")]
		[Range(0, 100, ErrorMessage = "MaxTemperature must be between 0 and 100.")]
		public decimal MaxTemperature { get; set; } // Nhiệt độ tối đa
		public string? Description { get; set; } // Mô tả sản phẩm
		[Range(0, 36, ErrorMessage = "WarrantyPeriod must be between 0 and 36 months.")]
		public int WarrantyPeriod { get; set; }//thời gian bảo hành
		[StringLength(50, ErrorMessage = "ModelNumber name cannot exceed 50 characters.")]
		public string ModelNumber { get; set; } = string.Empty; // Số model

	}
}
