using DTO.DTOs.Seeds;

namespace Service.Services
{
	public class SeedService : ISeedService
	{
		private readonly IUnitOfWork  _unitOfWork;
		private readonly IMapper _mapper;
		public SeedService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
		}
		public async Task<SeedDTO> CreateSeedAsync(SeedCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(nameof(dto));
			var seed = _mapper.Map<Seed>(dto);
			seed.CreatedAt = DateTime.Now;
			await _unitOfWork.Seeds.AddAsync(seed);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<SeedDTO>(dto);
		}

		public async Task<IEnumerable<SeedDTO>> GetAllSeedAsync()
		{
			var seeds = await _unitOfWork.Seeds.GetAllAsync();
			if(seeds.Any()||seeds==null) return new List<SeedDTO>();

			return _mapper.Map<IEnumerable<SeedDTO>>(seeds);
		}

		public async Task<SeedDTO> GetSeedByIdAsync(int id)
		{
			var seed = await _unitOfWork.Seeds.GetByIdAsync(id) ?? throw new NotFoundException($"Seed with Id {id} not found"); 
			return _mapper.Map<SeedDTO>(seed);

		}

		public async Task<SeedDTO> UpdateSeedAsync(int id,SeedUpdateDTO dto)
		{
			var existing = await _unitOfWork.Seeds.GetByIdAsync(id);
			if (existing == null)
				throw new NotFoundException($"Seed with Id {id} not found");

			_mapper.Map(dto, existing);
			existing.UpdatedAt = DateTime.Now;

			await _unitOfWork.Seeds.UpdateAsync(id,existing);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<SeedDTO>(existing);
		}
	}
}
