namespace Service.Services
{
	public class PowerSupplyService : IPowerSupplyService
	{	private readonly IMapper _mapper;
		private readonly IPowerSupplyRepository _repository;
		public PowerSupplyService(IPowerSupplyRepository repository,IMapper mapper)
		{
			_repository = repository;
			this._mapper = mapper;
		}
		// add a new power supply
		public async Task<PowerSupplyDTO?> AddPowerSupplyAsync(PowerSupplyCreateDTO createPowerSupplyDTO)
		{   //check null
			ArgumentNullException.ThrowIfNull(createPowerSupplyDTO);
			//check if Voltage is negative or zero
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(createPowerSupplyDTO.Voltage, nameof(createPowerSupplyDTO.Voltage));

			var entity = _mapper.Map<PowerSupply>(createPowerSupplyDTO);
			entity.CreatedAt = DateTime.Now; // Set the creation timestamp
			await _repository.AddAsync(entity);
			var result = _mapper.Map<PowerSupplyDTO>(entity);
			return result;
		}
		// get all power supplies
		public async Task<IEnumerable<PowerSupplyDTO>> GetAllPowerSuppliesAsync()
		{
			var powerSupplies = await _repository.GetAllAsync();
			if(powerSupplies== null || !powerSupplies.Any())
			{
				return new List<PowerSupplyDTO>();
			}

			return _mapper.Map<IEnumerable<PowerSupplyDTO>>(powerSupplies);
		}
		// get power supply by id
		public async Task<PowerSupplyDTO?> GetPowerSupplyByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var entity = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Power supply with ID {id} not found.");
			return _mapper.Map<PowerSupplyDTO>(entity);
		}
		// update power supply
		public async Task<PowerSupplyDTO?> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDTO updatePowerSupplyDTO)
		{

			var powerSupplExists = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Power supply with ID {id} not found."); 
			var entity = _mapper.Map<PowerSupply>(updatePowerSupplyDTO);
			_mapper.Map(updatePowerSupplyDTO, entity);
			entity.UpdatedAt = DateTime.Now;
			await _repository.UpdateAsync(entity.PowerSupplyId,entity);
			return _mapper.Map<PowerSupplyDTO>(entity);

		}
	}
}
