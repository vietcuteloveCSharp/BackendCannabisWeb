namespace Service.IServices
{
	public interface INutrientTypeService
	{
		Task<IEnumerable<NutrientTypeDTO>> GetAllNutrientTypeAsync();
		Task<NutrientTypeDTO?> GetNutrientTypeByIdAsync(int id);
		Task<NutrientTypeDTO> CreateNutrientTypeAsync(NutrientTypeCreateDTO dto);
		Task<NutrientTypeDTO> UpdateNutrientTypeAsync(int id, NutrientTypeUpdateDTO dto);
	}
}
