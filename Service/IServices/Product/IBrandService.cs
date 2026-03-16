
using DTO.DTOs.Breeders;

namespace Service.IServices.Product
{
	public interface IBrandService
	{
		public Task<IEnumerable<BrandDTO>> GetAllAsync();
		public Task<IEnumerable<BrandDTO>> GetAllActiveAsync();
		public Task<BrandDTO?> GetByIdAsync(int id);
		public Task<BrandDTO?> AddAsync(BrandCreateDTO brandDTO);
		public Task<bool> UpdateAsync(int id,BrandUpdateDTO brandDTO);
		public Task<bool> DeleteAsync(int id);

	}
}
