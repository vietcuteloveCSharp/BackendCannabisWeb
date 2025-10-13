namespace Service.IServices
{
	public interface IPowerSupplyService
	{
		Task<IEnumerable<PowerSupplyDTO>> GetAllPowerSuppliesAsync();
		Task<PowerSupplyDTO?> GetPowerSupplyByIdAsync(int id);
		Task<PowerSupplyDTO?> AddPowerSupplyAsync(PowerSupplyCreateDTO createPowerSupplyDTO);
		Task<PowerSupplyDTO?> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDTO updatePowerSupplyDTO);
	}
}
