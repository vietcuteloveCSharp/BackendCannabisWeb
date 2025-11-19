using DTO.DTOs.GrowLights;

namespace Service.Services
{
	public class GrowLightService : IGrowLightService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public GrowLightService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this._unitOfWork = unitOfWork;	
			this._mapper = mapper;	
		}
		public async Task<GrowLightDTO> CreateGrowLightAsync(GrowLightCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var entity = _mapper.Map<GrowLight>(dto);
			entity.CreatedAt = DateTime.Now;

			await _unitOfWork.GrowLights.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<GrowLightDTO>(dto);
;		}

		public async Task<IEnumerable<GrowLightDTO>> GetAllGrowLightAsync()
		{
			var growLights = await _unitOfWork.GrowLights.GetAllAsync();
			if(growLights == null || growLights.Any())  return new List<GrowLightDTO>();
			return _mapper.Map<IEnumerable<GrowLightDTO>>(growLights);
		}

		public async Task<GrowLightDTO> GetGrowLightByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var growLight = await _unitOfWork.GrowLights.GetByIdAsync(id);
			if (growLight == null) throw new NotFoundException($"Grow Light with ID {id} not found");
			return _mapper.Map<GrowLightDTO>(growLight);
		}

		//public async Task<GrowLightDTO> UpdateGrowLightAsync(int id, GrowLightUpdateDTO dto)
		//{
		//	ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id,nameof(id));
		//	var existing = await _unitOfWork.GrowLights.GetByIdAsync(id)?? throw new NotFoundException($"Grow Light with ID {id} not found");
		//	_mapper.Map(dto, existing);
		//	existing.UpdatedAt=DateTime.Now;

		//	await _unitOfWork.GrowLights.UpdateAsync(id, existing);
		//	await _unitOfWork.SaveChangesAsync();
		//	return _mapper.Map<GrowLightDTO>(existing);
		//}
	}
}
