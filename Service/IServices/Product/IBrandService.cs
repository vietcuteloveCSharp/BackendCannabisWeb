
using DTO.DTOs.Breeders;

namespace Service.IServices.Product
{
	public interface IBrandService
	{
		public Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
		public Task<BrandDTO?> GetBrandByIdAsync(int id);
		public Task<BrandDTO?> AddBrandAsync(BrandCreateDTO brandDTO);
		public Task<bool> UpdateBrandAsync(int id,BrandUpdateDTO brandDTO);

	}
}
