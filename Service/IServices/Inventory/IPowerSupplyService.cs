namespace Service.IServices.Inventory
{
	public interface IPowerSupplyService
	{
		Task<IEnumerable<PowerSupplyDTO>> GetAllAsync();
		Task<IEnumerable<PowerSupplyDTO>> GetAllActiveAsync();
		Task<PowerSupplyDTO?> GetByIdAsync(int id);
		Task<PowerSupplyDTO> AddAsync(PowerSupplyCreateDTO createPowerSupplyDTO);
		Task<bool> UpdateAsync(int id, PowerSupplyUpdateDTO updatePowerSupplyDTO);
		Task<bool> ExistsAsync(int id);
		Task<bool> DeleteAsync(int id);
	}
}
