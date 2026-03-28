namespace Service.IServices.Inventory
{
	public interface INutrientTypeService :IBaseService<NutrientType,NutrientTypeDTO,NutrientTypeCreateDTO,NutrientTypeUpdateDTO>
	{
		Task<bool> NameExists(string name);
		
	}
}
