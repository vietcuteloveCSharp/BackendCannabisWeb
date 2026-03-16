namespace Service.IServices.Inventory
{
	public interface INutrientTypeService
	{
		Task<IEnumerable<NutrientTypeDTO>> GetAllAsync();
		Task<IEnumerable<NutrientTypeDTO>> GetAllActiveAsync();
		Task<NutrientTypeDTO?> GetByIdAsync(int id);
		Task<NutrientTypeDTO> CreateAsync(NutrientTypeCreateDTO dto);
		Task<bool> UpdateAsync(int id, NutrientTypeUpdateDTO dto);
		Task<bool> NameExist(string name);
		Task<bool> DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
	}
}
