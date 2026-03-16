using DTO.DTOs.ChipModels;
using Service.IServices.Inventory;

namespace Service.Services.Inventory
{
	public class ChipModelService(IUnitOfWork unitOfWork, IMapper mapper) : IChipModelService
	{
		private readonly IUnitOfWork _unitOfWork =unitOfWork;
		private readonly IMapper _mapper=mapper;
		public async Task<ChipModelDTO> CreateAsync(ChipModelCreateDTO dto)
		{
			var chipmodelEntity = _mapper.Map<ChipModel>(dto);
			 await _unitOfWork.ChipModels.AddAsync(chipmodelEntity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<ChipModelDTO>(chipmodelEntity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.ChipModels.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.ChipModels.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
		
		public async Task<bool> ExistAsync(int id)
		{
			var item = await _unitOfWork.ChipModels.GetByIdAsync(id);
			return item != null && !item.IsDeleted;
		}

		public async Task<IEnumerable<ChipModelDTO>> GetAllActiveAsync()
		{
			var chipModels = await _unitOfWork.ChipModels.GetAllActiveAsync();
			if (chipModels == null || !chipModels.Any())
			{
				return new List<ChipModelDTO>();
			}
			return _mapper.Map<IEnumerable<ChipModelDTO>>(chipModels);
		}

		public async Task<IEnumerable<ChipModelDTO>> GetAllAsync()
		{
			var chipModels = await _unitOfWork.ChipModels.GetAllAsync();
			if (chipModels == null || !chipModels.Any())
			{
				return new List<ChipModelDTO>();
			}
			return _mapper.Map<IEnumerable<ChipModelDTO>>(chipModels);
		}

		public async Task<ChipModelDTO?> GetByIdAsync(int id)
		{ 
			var chipModel = await _unitOfWork.ChipModels.GetByIdAsync(id) ?? throw new NotFoundException($"ChipModel with Id:{id} not found");
			return _mapper.Map<ChipModelDTO>(chipModel);
		}
		//update chip model
		public async Task<bool> UpdateAsync(int id, ChipModelUpdateDTO dto)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var chipModel = await _unitOfWork.ChipModels.GetByIdAsync(id) ?? throw new NotFoundException($"ChipModel with Id:{id} not found");
			_mapper.Map(dto, chipModel);
			chipModel.UpdatedAt = DateTime.Now;
			 _unitOfWork.ChipModels.Update(chipModel);
			await _unitOfWork.SaveChangesAsync();
			return true;

		}
	}
}
