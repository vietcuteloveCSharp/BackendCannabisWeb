namespace Service.IServices
{
	public interface ICarbonFilterService
	{
		Task<IEnumerable<CarbonFilterDTO>> GetAllCarbonFilterAsync();
		Task<CarbonFilterDTO> AddCarbonFilterAsync(CarbonFilterCreateDTO createCarbonFilterDTO);
		Task<CarbonFilterDTO?> GetCarbonFilterByIdAsync(int id);
		Task<CarbonFilterDTO?> UpdateCarbonFilterAsync(int id, CarbonFilterUpdateDTO updateCarbonFilterDTO);
	}
}
