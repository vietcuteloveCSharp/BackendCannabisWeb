namespace Service.IServices
{
	public interface IGrowTentService
	{
		Task<IEnumerable<GrowTentDTO>> GetAllAsync();
		Task<GrowTentDTO?> GetByIdAsync(int id);
		Task<GrowTentDTO> CreateAsync(GrowTentCreateDTO dto);
		Task<GrowTentDTO> UpdateAsync(int id, GrowTentUpdateDTO dto);
	}
}
