
namespace Service.Services
{
	public class ClassificationService : IClassificationService
	{	private readonly IClassificationRepository _repository;
		private readonly IMapper _mapper;
		public ClassificationService(IClassificationRepository repository, IMapper _mapper)
		{
			this._repository = repository;
			this._mapper = _mapper;
		}
		// Create a new classification
		public async Task<ClassificationDTO> CreateClassificationAsync(CreateClassificationDTO dto)
		{
			ArgumentNullException.ThrowIfNull(nameof(dto));
			var entity = _mapper.Map<Classification>(dto);
			entity.CreatedAt = DateTime.Now; // Set the creation timestamp
			await _repository.AddAsync(entity);
			return _mapper.Map<ClassificationDTO>(entity);
		}
		// Get all classifications
		public async Task<IEnumerable<ClassificationDTO>> GetAllClassificationAsync()
		{
			var classifications = await _repository.GetAllAsync();
			if (classifications == null)
			{
				return new List<ClassificationDTO>();
			}
			return _mapper.Map<List<ClassificationDTO>>(classifications);
		}
		// Get classification by ID
		public async Task<ClassificationDTO?> GetByIdAsync(int id)
		{	
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var entity = await _repository.GetByIdAsync(id);
			if(entity == null)
			{
				throw new NotFoundException($"Classification with ID {id} not found.");
			}
			return _mapper.Map<ClassificationDTO>(entity);
		}
		// Update classification
		public async Task<ClassificationDTO> UpdateClassificationAsync(int id, UpdateClassificationDTO dto)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				throw new NotFoundException($"Classification with ID {id} not found.");
			}
			_mapper.Map(dto, entity);
			entity.UpdatedAt = DateTime.Now; // Update the timestamp
			await _repository.UpdateAsync(id,entity);
			return _mapper.Map<ClassificationDTO>(entity);
		}
	}
}
