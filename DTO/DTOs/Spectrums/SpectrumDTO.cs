using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.Spectrums
{
	public class SpectrumDTO
	{
		public int Id { get; set; }
		public ESpectrumType Type { get; set; }
		public string? ColorHexCode { get; set; } // Màu đại diện (ví dụ: Hồng tím cho Bloom)
		public string? SpectrumChartUrl { get; set; } // Ảnh biểu đồ bước sóng
		public string? Description { get; set; }
		public int? ColorTemperatureK { get; set; }
		public int? CRI { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
