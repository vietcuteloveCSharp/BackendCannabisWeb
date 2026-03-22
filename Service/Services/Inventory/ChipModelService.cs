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
			var entity = await _unitOfWork.ChipModels.GetByIdAsync(id)
			?? throw new NotFoundException($"ChipModel ID {id} không tồn tại.");

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
		//get chip model by id
		public async Task<ChipModelDTO?> GetByIdAsync(int id)
		{
			var chipModel = await _unitOfWork.ChipModels.GetByIdAsync(id);
			if (chipModel == null || chipModel.IsDeleted) return null;
			return _mapper.Map<ChipModelDTO>(chipModel);
		}
		//update chip model
		public async Task<bool> UpdateAsync(int id, ChipModelUpdateDTO dto)
		{
			// 1. Validate ID nhanh 
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

			// 2. Tìm Entity. Nếu null, ném NotFoundException để Middleware tự xử lý trả về 404.
			// Dùng GetByIdAsync (FindAsync) ở đây là hợp lý vì ta cần Tracking để Update.
			var chipModel = await _unitOfWork.ChipModels.GetByIdAsync(id)
				?? throw new NotFoundException($"ChipModel ID {id} không tồn tại.");

			// 3. Map đè dữ liệu từ DTO vào Entity đã được tracking. 
			// AutoMapper sẽ chỉ thay đổi các field có trong DTO, giữ nguyên các field khác.
			_mapper.Map(dto, chipModel);

			// 4. Cập nhật thời gian (luôn dùng UtcNow)
			chipModel.UpdatedAt = DateTime.UtcNow;

			// 5. Đánh dấu Update và Save. 
			// nhưng gọi để tường minh và đảm bảo trạng thái EntityState.Modified.
			_unitOfWork.ChipModels.Update(chipModel);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
	}
}
