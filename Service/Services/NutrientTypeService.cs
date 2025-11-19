namespace Service.Services
{
	public class NutrientTypeService :INutrientTypeService
	{
		private readonly INutrientTypeRepository _repository;
		private readonly IMapper _mapper;
		public NutrientTypeService(INutrientTypeRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		// Create a new nutrient type
		public async Task<NutrientTypeDTO> CreateNutrientTypeAsync(NutrientTypeCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var nutrientType = _mapper.Map<NutrientType>(dto);
			nutrientType.CreatedAt = DateTime.Now; // Set the creation timestamp
			await _repository.AddAsync(nutrientType);
			return _mapper.Map<NutrientTypeDTO>(nutrientType);
		}
		//get list of all nutrient types
		public async Task<IEnumerable<NutrientTypeDTO>> GetAllNutrientTypeAsync()
		{
			var nutrientTypes = await _repository.GetAllAsync();
			if(nutrientTypes == null || !nutrientTypes.Any())
			{
				return  new List<NutrientTypeDTO>();
			}
			return _mapper.Map<IEnumerable<NutrientTypeDTO>>(nutrientTypes);
		}
		// Get nutrient type by id
		public async Task<NutrientTypeDTO?> GetNutrientTypeByIdAsync(int id)
		{	
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id)); //check xem id có âm k
			var nutrientType = await _repository.GetByIdAsync(id);
			if (nutrientType == null)
			{
				throw new NotFoundException($"Nutrient type with ID {id} not found.");
			}
			return _mapper.Map<NutrientTypeDTO>(nutrientType);
		}
		//update nutrient type
		public async Task<NutrientTypeDTO> UpdateNutrientTypeAsync(int id, NutrientTypeUpdateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto)); //check dto is null
			var nutrientType = await _repository.GetByIdAsync(id);
			if (nutrientType == null) throw new NotFoundException($"Nutrient type with ID {id} not found.");

			_mapper.Map(dto, nutrientType);
			nutrientType.UpdatedAt = DateTime.Now; //update time
			_repository.Update(nutrientType);
			return _mapper.Map<NutrientTypeDTO>(nutrientType);
		}
	}
}
