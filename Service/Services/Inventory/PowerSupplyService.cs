using Service.IServices.Inventory;

namespace Service.Services.Inventory
{
	public class PowerSupplyService : IPowerSupplyService
	{	private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public PowerSupplyService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		// add a new power supply
		public async Task<PowerSupplyDTO> AddAsync(PowerSupplyCreateDTO createPowerSupplyDTO)
		{   
			var entity = _mapper.Map<PowerSupply>(createPowerSupplyDTO);
			await _unitOfWork.PowerSupplies.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<PowerSupplyDTO>(entity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.PowerSupplies.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.PowerSupplies.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public  async Task<bool> ExistsAsync(int id)
		{
			var item = await _unitOfWork.PowerSupplies.GetByIdAsync(id);
			return item != null && !item.IsDeleted;
		}

		public async Task<IEnumerable<PowerSupplyDTO>> GetAllActiveAsync()
		{
			var powerSupplies = await _unitOfWork.PowerSupplies.GetAllActiveAsync();
			if (powerSupplies == null || !powerSupplies.Any())
			{
				return new List<PowerSupplyDTO>();
			}

			return _mapper.Map<IEnumerable<PowerSupplyDTO>>(powerSupplies);
		}

		// get all power supplies
		public async Task<IEnumerable<PowerSupplyDTO>> GetAllAsync()
		{
			var powerSupplies = await _unitOfWork.PowerSupplies.GetAllAsync();
			if(powerSupplies== null || !powerSupplies.Any())
			{
				return new List<PowerSupplyDTO>();
			}

			return _mapper.Map<IEnumerable<PowerSupplyDTO>>(powerSupplies);
		}
		
		// get power supply by id
		public async Task<PowerSupplyDTO?> GetByIdAsync(int id)
		{
			var entity = await _unitOfWork.PowerSupplies.GetByIdAsync(id) ?? throw new NotFoundException($"Power supply with ID {id} not found.");
			return _mapper.Map<PowerSupplyDTO>(entity);
		}
		// update power supply
		public async Task<bool> UpdateAsync(int id, PowerSupplyUpdateDTO updatePowerSupplyDTO)
		{
			var powerSupplExists = await _unitOfWork.PowerSupplies.GetByIdAsync(id) ?? throw new NotFoundException($"Power supply with ID {id} not found.");
			var entity = _mapper.Map<PowerSupply>(updatePowerSupplyDTO);
			_mapper.Map(updatePowerSupplyDTO, entity);
			entity.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.PowerSupplies.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;

		}
	}
}
