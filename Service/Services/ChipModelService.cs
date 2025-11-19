using DTO.DTOs.ChipModels;

namespace Service.Services
{
	public class ChipModelService : IChipModelService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ChipModelService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public Task<ChipModelDTO> CreateChipModelAsync(ChipModelCreateDTO dto)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ChipModelDTO>> GetAllChipModelAsync()
		{
			var chipModels = await _unitOfWork.ChipModels.GetAllAsync();
			if (chipModels == null || !chipModels.Any())
			{
				return new List<ChipModelDTO>();
			}
			return _mapper.Map<IEnumerable<ChipModelDTO>>(chipModels);
		}

		public async Task<ChipModelDTO> GetChipModelByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var chipModel = await _unitOfWork.ChipModels.GetByIdAsync(id) ?? throw new NotFoundException($"ChipModel with Id:{id} not found");
			return _mapper.Map<ChipModelDTO>(chipModel);
		}
		////update chip model
		//public async Task<ChipModelDTO> UpdateChipModelAsync(int id,ChipModelUpdateDTO dto)
		//{	
		//	ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
		//	var chipModel = await _unitOfWork.ChipModels.GetByIdAsync(id) ?? throw new NotFoundException($"ChipModel with Id:{id} not found");
		//	_mapper.Map(dto, chipModel);
		//	chipModel.UpdatedAt = DateTime.Now;
		//	await _unitOfWork.ChipModels.UpdateAsync(id, chipModel);
		//	await _unitOfWork.SaveChangesAsync();
		//	return _mapper.Map<ChipModelDTO>(chipModel);

		//}
	}
}
