namespace Service.IServices.Product
{
	public interface IClassificationService
	{
		Task<IEnumerable<ClassificationDTO>> GetAllAsync();
		Task<IEnumerable<ClassificationDTO>> GetAllActiveAsync();
		Task<ClassificationDTO?> GetByIdAsync(int id);
		Task<ClassificationDTO> CreateAsync(ClassificationCreateDTO dto);
		Task<bool> UpdateAsync(int id, ClassificationUpdateDTO dto);
		Task<bool> DeleteAsync(int id);
		Task<bool> NameExistsAsync(string classifiName);
		
	}
}
