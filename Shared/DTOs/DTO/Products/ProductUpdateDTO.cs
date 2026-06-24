

namespace Shared.DTOs.DTO.Products
{
	public class ProductUpdateDTO
	{
		[Required(ErrorMessage = "Tên sản phẩm không được để trống")]
		[StringLength(255)]
		public string ProductName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Danh mục là bắt buộc")]
		public int CategoryId { get; set; }

		public int? BrandId { get; set; }

		[StringLength(50)]
		public string? ProductType { get; set; }
	}
}
