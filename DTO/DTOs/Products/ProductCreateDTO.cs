using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.Products
{
	public class ProductCreateDTO
	{
		[Required]
		[StringLength(255)]
		public string ProductName { get; set; } = string.Empty;

		[Required]
		public int CategoryId { get; set; }

		public int? BrandId { get; set; }

		[StringLength(50)]
		public string? ProductType { get; set; } // Ví dụ: 'Seed', 'Light', 'Nutrient'
	}
}
