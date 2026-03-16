namespace Service.Services
{
	public class GrowTentService :IGrowTentService
	{ 
		private readonly IGrowTentRepository _repository;
		private readonly IMapper _mapper;
		public GrowTentService(IGrowTentRepository repository, IMapper mapper)
		{
				this._repository = repository;
				this._mapper = mapper;	
		}
		// Create a new grow tent
		public async Task<GrowTentDTO> CreateAsync(GrowTentCreateDTO dto)
		{	//check dto is null
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			//check BrandId, Price, Quantity are negative or zero
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dto.BrandId, nameof(dto.BrandId));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dto.Price, nameof(dto.Price));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dto.Quantity, nameof(dto.Quantity));
			var entity = _mapper.Map<GrowTent>(dto);
			entity.CreatedAt = DateTime.Now; // Set the creation timestamp
			await _repository.AddAsync(entity);
			var growTent = _mapper.Map<GrowTentDTO>(entity);
			return growTent;

		}
		// Get all grow tents
		public async  Task<IEnumerable<GrowTentDTO>> GetAllAsync()
		{
			var growTents = await _repository.GetAllAsync();
			if (growTents == null || !growTents.Any())
			{
				return new List<GrowTentDTO>();
			}
			return _mapper.Map<IEnumerable<GrowTentDTO>>(growTents);
		}
		// Get grow tent by ID
		public async Task<GrowTentDTO?> GetByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var growTent = await _repository.GetByIdAsync(id);
			if (growTent == null)
			{
				throw new NotFoundException($"Grow tent with ID {id} not found.");
			}
			return _mapper.Map<GrowTentDTO>(growTent);
		}
		// Update grow tent
		public async Task<GrowTentDTO> UpdateAsync(int id, GrowTentUpdateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dto.BrandId, nameof(dto.BrandId));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dto.Price, nameof(dto.Price));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dto.Quantity, nameof(dto.Quantity));
			var growTent = await _repository.GetByIdAsync(id);
			if (growTent == null)
			{
				throw new NotFoundException($"Grow tent with ID {id} not found.");
			}
			_mapper.Map(dto, growTent);
			growTent.UpdatedAt = DateTime.Now;

			_repository.Update(growTent);
			return _mapper.Map<GrowTentDTO>(growTent);
		}
	}
}
