namespace Service.Services
{
	public class CarbonFilterService : ICarbonFilterService
	{	private readonly ICarbonFilterRepository _repository;
		private readonly IMapper _mapper;
		private readonly IBrandRepository _brandRepository;
		public CarbonFilterService(ICarbonFilterRepository _repository, IMapper mapper, IBrandRepository brandRepository)
		{
			this._repository = _repository;
			_mapper = mapper;
			_brandRepository = brandRepository;
		}
		//ad a new carbon filter
		public async Task<CarbonFilterDTO> AddCarbonFilterAsync(CarbonFilterCreateDTO createCarbonFilterDTO)
		{
			ArgumentNullException.ThrowIfNull(createCarbonFilterDTO);
			//check if brand exists
			var brandExists = await _brandRepository.ExistsAsync(createCarbonFilterDTO.BrandId);
			if (brandExists)
			{
				throw new ArgumentException("The brand does not exist.", nameof(createCarbonFilterDTO.BrandId));
			}
			if (createCarbonFilterDTO.MinTemperature > createCarbonFilterDTO.MaxTemperature)
			{
				throw new ArgumentException("The minimum temperature must not be greater than the maximum temperature.");
			}
			// Map DTO to Entity
			var carbonFilter = _mapper.Map<CarbonFilter>(createCarbonFilterDTO);
			carbonFilter.CreatedAt = DateTime.Now; //set up time
			await _repository.AddAsync(carbonFilter);
			// Map Entity back to DTO
			var carbonFilterDTO = _mapper.Map<CarbonFilterDTO>(carbonFilter);
			return carbonFilterDTO;

		}
		//get all carbon filters
		public async Task<IEnumerable<CarbonFilterDTO>> GetAllCarbonFilterAsync()
		{
			var carbonFilters = await _repository.GetAllAsync();
			if(carbonFilters == null || !carbonFilters.Any())
			{
				return new List<CarbonFilterDTO>();
			}
			return _mapper.Map<IEnumerable<CarbonFilterDTO>>(carbonFilters);
		}
		// get carbon filter by id
		public async Task<CarbonFilterDTO?> GetCarbonFilterByIdAsync(int id)
		{	
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var  entity = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Carbon filter with ID {id} not found.");
			return _mapper.Map<CarbonFilterDTO>(entity);
		}
		// update carbon filter
		public async Task<CarbonFilterDTO?> UpdateCarbonFilterAsync(int id, CarbonFilterUpdateDTO updateCarbonFilterDTO)
		{
			ArgumentNullException.ThrowIfNull(updateCarbonFilterDTO);

			var existingEntity = await _repository.GetByIdAsync(id)
				?? throw new NotFoundException($"Carbon filter with ID {id} not found.");
			// Kiểm tra nhiệt độ
			if (updateCarbonFilterDTO.MinTemperature > updateCarbonFilterDTO.MaxTemperature)
			{
				throw new ArgumentException("The minimum temperature must not be greater than the maximum temperature.");
			}
			// Assumed: BrandId validity is guaranteed by frontend (already checked with GET /brands/{id})
			// Map DTO to  entity 
			_mapper.Map(updateCarbonFilterDTO, existingEntity);
			_repository.Update(existingEntity);

			return _mapper.Map<CarbonFilterDTO>(existingEntity);
		}
	}
}
