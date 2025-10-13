namespace Service.Services
{
	public interface IClassificationService
	{
		Task<IEnumerable<ClassificationDTO>> GetAllClassificationAsync();
		Task<ClassificationDTO?> GetByIdAsync(int id);
		Task<ClassificationDTO> CreateClassificationAsync(CreateClassificationDTO dto);
		Task<ClassificationDTO> UpdateClassificationAsync(int id, UpdateClassificationDTO dto);
	}
}
