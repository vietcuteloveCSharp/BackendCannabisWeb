namespace Service.IServices
{
	public interface ISpectrumService
	{
		Task<IEnumerable<SpectrumDTO>> GetAllSpectrumsAsync();
		Task<SpectrumDTO?> GetSpectrumByIdAsync(int id);
		Task<SpectrumDTO?> AddSpectrumAsync(SpectrumCreateDTO createSpectrumDTO);
		Task<SpectrumDTO?> UpdateSpectrumAsync(int id, SpectrumUpdateDTO updateSpectrumDTO);
	}
}
