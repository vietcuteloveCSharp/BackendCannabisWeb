using DTO.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.Product
{
	public interface IProductService
	{
		Task<ProductDTO?> GetByIdAsync(int id);
		Task<IEnumerable<ProductDTO>> GetAllAsync();
		Task<IEnumerable<ProductDTO>> GetAllActiveAsync();
		Task<ProductDTO> CreateAsync(ProductCreateDTO dto);
		Task<bool> UpdateAsync(int id, ProductUpdateDTO dto);
		Task<bool> ToggleActiveAsync(int productId, bool isActive);
		Task<bool> DeleteAsync(int id);
	}
}
