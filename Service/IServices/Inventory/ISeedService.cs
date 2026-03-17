using DTO.DTOs.Seeds;

namespace Service.IServices.Inventory
{
	public interface ISeedService
	{
		Task<IEnumerable<SeedDTO>> GetAllAsync();
		Task<IEnumerable<SeedDTO>> GetAllActiveAsync();
		Task<SeedDTO?> GetByIdAsync(int id);
		Task<SeedDTO> CreateAsync(SeedCreateRequestDTO dto);
		Task<bool> UpdateAsync(int id, SeedUpdateDTO dto);
		Task<bool> DeleteAsync(int id);
	}
}
