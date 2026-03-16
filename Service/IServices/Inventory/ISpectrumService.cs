namespace Service.IServices
{
	public interface ISpectrumService
	{
		Task<IEnumerable<SpectrumDTO?>> GetAllAsync();
		Task<IEnumerable<SpectrumDTO?>> GetAllActiveAsync();
		Task<SpectrumDTO?> GetByIdAsync(int id);
		Task<SpectrumDTO> AddAsync(SpectrumCreateDTO createSpectrumDTO);
		Task<bool> UpdateAsync(int id, SpectrumUpdateDTO updateSpectrumDTO);
		Task<bool> DeleteAsync(int id);
		Task<bool> ExistAsync(int id);
	}
}
