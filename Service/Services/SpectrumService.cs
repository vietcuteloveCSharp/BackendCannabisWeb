namespace Service.Services
{
	public class SpectrumService : ISpectrumService
	{
		private readonly ISpectrumRepository _repository;
		private readonly IMapper _mapper;
		public SpectrumService(ISpectrumRepository _repository, IMapper mapper)
		{
			this._repository = _repository;
			this._mapper = mapper;
		}
		// add a new spectrum
		public async Task<SpectrumDTO?> AddSpectrumAsync(SpectrumCreateDTO createSpectrumDTO)
		{
			//check if createSpectrumDTO is null
			ArgumentNullException.ThrowIfNull(createSpectrumDTO, nameof(createSpectrumDTO));
			var entity = _mapper.Map<Spectrum>(createSpectrumDTO);
			entity.CreatedAt = DateTime.Now; // Set the creation timestamp
			var added = await _repository.AddAsync(entity);
			return _mapper.Map<SpectrumDTO>(added);
		}
		// get all spectrums
		public async Task<IEnumerable<SpectrumDTO>> GetAllSpectrumsAsync()
		{
			var spectrums = await _repository.GetAllAsync();
			if (spectrums == null || !spectrums.Any())
			{
				return new List<SpectrumDTO>();
			}
			return _mapper.Map<IEnumerable<SpectrumDTO>>(spectrums);
		}
		// get spectrum by id
		public async Task<SpectrumDTO?> GetSpectrumByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var spectrum = await _repository.GetByIdAsync(id);
			if (spectrum == null)
			{
				throw new NotFoundException($"Spectrum with ID {id} not found.");
			}
			return _mapper.Map<SpectrumDTO>(spectrum);
		}
		// update spectrum
		public async Task<SpectrumDTO?> UpdateSpectrumAsync(int id, SpectrumUpdateDTO updateSpectrumDTO)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null)
			{
				throw new NotFoundException($"Spectrum with ID {id} not found.");
			}
			var entity = _mapper.Map<Spectrum>(updateSpectrumDTO);
			var updated = await _repository.UpdateAsync(entity.SpectrumId, entity);
			if (updated == null)
			{
				throw new Exception($"Failed to update spectrum with ID {id}.");
			}
			return _mapper.Map<SpectrumDTO>(updated);
		}
	}
}
