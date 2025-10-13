

using DTO.DTOs.CoolingSystems;

namespace Service.Services
{
	public class CoolingSystemService : ICoolingSystemService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CoolingSystemService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork =unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<CoolingSystemDTO> CreateCoolingSystemAsync(CoolingSystemCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var entity = _mapper.Map<CoolingSystem>(dto);
			entity.CreatedAt = DateTime.Now; // Set the creation timestamp
			await _unitOfWork.CoolingSystems.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<CoolingSystemDTO>(entity);

		}

		public	async Task<IEnumerable<CoolingSystemDTO>> GetAllCoolingSystemAsync()
		{
			var coolingSystems = await _unitOfWork.CoolingSystems.GetAllAsync();
			if(coolingSystems == null || !coolingSystems.Any())
			{
				return new List<CoolingSystemDTO>();
			}
			return _mapper.Map<IEnumerable<CoolingSystemDTO>>(coolingSystems);
		}

		public async Task<CoolingSystemDTO> GetCoolingSystemByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var coolingSystem = await _unitOfWork.CoolingSystems.GetByIdAsync(id) ?? throw new NotFoundException($"Cooling System  with ID {id} not found.");
			return _mapper.Map<CoolingSystemDTO>(coolingSystem);
		}

		public async Task<CoolingSystemDTO> UpdateCoolingSystemAsync(int id, CoolingSystemUpdateDTO dto)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var entity = await _unitOfWork.CoolingSystems.GetByIdAsync(id)?? throw  new NotFoundException($"Cooling System  with ID {id} not found."); 

			_mapper.Map(dto, entity);
			entity.UpdatedAt = DateTime.Now; // Update the timestamp
			await _unitOfWork.CoolingSystems.UpdateAsync(id, entity);
			return _mapper.Map<CoolingSystemDTO>(entity);

		}
	}
}
