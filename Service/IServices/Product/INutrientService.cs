namespace Service.IServices.Product
{
	public interface INutrientService
	{	
		Task<IEnumerable<NutrientDTO?>> GetAllAsync();
		Task<IEnumerable<NutrientDTO?>> GetAllActiveAsync();
		Task<NutrientDTO> CreateAsync(NutrientCreateDTO dto);
		Task<bool> UpdateAsync(int id, NutrientUpdateDTO dto);
		Task<NutrientDTO?> GetByIdAsync(int id);
		Task<bool> DeleteAsync(int id);
		Task<bool> ExistAsync(int id);
	}
}
