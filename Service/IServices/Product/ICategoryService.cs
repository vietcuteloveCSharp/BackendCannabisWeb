namespace Service.IServices.Product
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDTO>> GetAllAsync();
		Task<CategoryDTO?> GetByIdAsync(int id);
		Task<CategoryDTO?> GetByNameAsync(string name);
		Task<CategoryDTO> AddAsync(CategoryCreateDTO createCategoryDTO);
		Task<bool> UpdateAsync(int id,CategoryUpdateDTO updateCategoryDTO);
		Task<bool> DeleteAsync(int id);
		
	}
}
