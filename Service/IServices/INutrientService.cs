namespace Service.IServices
{
	public interface INutrientService
	{	
		Task<IEnumerable<NutrientDTO>> GetAllNutrientAsync();
		Task<NutrientDTO> CreateNutrientAsync(NutrientCreateDTO dto);
		Task<NutrientDTO> UpdateNutrientAsync(int id, NutrientUpdateDTO dto);
		Task<NutrientDTO> GetNutrientByIdAsync(int id);
	}
}
