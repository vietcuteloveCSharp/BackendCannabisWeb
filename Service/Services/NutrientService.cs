namespace Service.Services
{
	public class NutrientService : INutrientService
	{
		private readonly INutrientRepository _repository;
		private readonly IMapper _mapper;
		public NutrientService(INutrientRepository repository, IMapper mapper)
		{
			this._repository = repository;
			this._mapper = mapper;
		}
		// Create a new nutrient
		public async Task<NutrientDTO> CreateNutrientAsync(NutrientCreateDTO dto)
		{
			// check if dto is null
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			// check if Quantity is negative or zero
			ArgumentOutOfRangeException.ThrowIfNegative(dto.Quantity, nameof(dto.Quantity));
			var nutrient = _mapper.Map<Nutrient>(dto);
			nutrient.CreatedAt = DateTime.Now; // Set the creation timestamp
			await _repository.AddAsync(nutrient);
			return _mapper.Map<NutrientDTO>(nutrient);
		}
		// Get all nutrients
		public async Task<IEnumerable<NutrientDTO>> GetAllNutrientAsync()
		{
			var nutrients = await _repository.GetAllAsync();
			if (nutrients == null || !nutrients.Any())
			{
				return new List<NutrientDTO>();
			}
			return _mapper.Map<IEnumerable<NutrientDTO>>(nutrients);
		}
		// Get nutrient by ID
		public async Task<NutrientDTO> GetNutrientByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var nutrient = await _repository.GetByIdAsync(id);
			if (nutrient == null)
			{
				throw new NotFoundException($"Nutrient with ID {id} not found.");
			}
			return _mapper.Map<NutrientDTO>(nutrient);
		}
		// Update nutrient
		public async Task<NutrientDTO> UpdateNutrientAsync(int id, NutrientUpdateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			ArgumentOutOfRangeException.ThrowIfNegative(dto.Quantity, nameof(dto.Quantity));

			var nutrient = await _repository.GetByIdAsync(id);
			if (nutrient == null)
			{
				throw new NotFoundException($"Nutrient with ID {id} not found.");
			}
			_mapper.Map(dto, nutrient);
			nutrient.UpdatedAt = DateTime.Now;
			await _repository.UpdateAsync(id, nutrient);
			return _mapper.Map<NutrientDTO>(nutrient);
		}
	}
}
