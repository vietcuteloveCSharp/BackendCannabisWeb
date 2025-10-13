using DTO.DTOs.Dehumidifiers;

namespace Service.Services
{
	public class DehumidifierService : IDehumidifierService
	{	private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public DehumidifierService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
		}
		public async Task<DehumidifierDTO> CreateDehumidifierAsync(DehumidifierCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var entity =_mapper.Map<Dehumidifier>(dto);
			entity.CreatedAt = DateTime.Now;
			await _unitOfWork.Dehumidifiers.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<DehumidifierDTO>(entity);
		}

		public async Task<IEnumerable<DehumidifierDTO>> GetAllDehumidifierAsync()
		{
			var dehumidifiers= await _unitOfWork.Dehumidifiers.GetAllAsync();
			if (dehumidifiers == null || dehumidifiers.Any()) return new List<DehumidifierDTO>();
			return _mapper.Map<IEnumerable<DehumidifierDTO>>(dehumidifiers);
		}

		public async Task<DehumidifierDTO> GetDehumidifierByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var entity = await _unitOfWork.Dehumidifiers.GetByIdAsync(id) ?? throw new NotFoundException($"Dehumidifier with Id {id} not found");
			return _mapper.Map<DehumidifierDTO>(entity);
			
		}

		public async Task<DehumidifierDTO> UpdateDehumidifierAsync(int id, DehumidifierUpdateDTO dto)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var entity = await _unitOfWork.Dehumidifiers.GetByIdAsync(id) ?? throw new NotFoundException($"Dehumidifier with Id {id} not found");
			_mapper.Map(dto, entity);
			entity.UpdatedAt=DateTime.Now;
			await _unitOfWork.Dehumidifiers.UpdateAsync(id,entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<DehumidifierDTO>(entity);
		}
	}
}
