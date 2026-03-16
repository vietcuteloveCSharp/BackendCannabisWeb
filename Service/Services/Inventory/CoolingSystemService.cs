

using DTO.DTOs.CoolingSystems;
using DTO.Response;
using Service.IServices.Inventory;

namespace Service.Services.Inventory
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

		public async Task<CoolingSystemDTO> CreateAsync(CoolingSystemCreateDTO dto)
		{
			var entity = _mapper.Map<CoolingSystem>(dto);
			await _unitOfWork.CoolingSystems.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<CoolingSystemDTO>(entity);

		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.CoolingSystems.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.CoolingSystems.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> ExistsAsync(int id)
		{
			var item = await _unitOfWork.CoolingSystems.GetByIdAsync(id);
			return item != null && !item.IsDeleted;
		}
		//get all active
		public async Task<IEnumerable<CoolingSystemDTO>> GetAllActiveAsync()
		{
			var items = await _unitOfWork.CoolingSystems.GetAllActiveAsync();
			return _mapper.Map<IEnumerable<CoolingSystemDTO>>(items);
		}
		//get all
		public async Task<IEnumerable<CoolingSystemDTO>> GetAllAsync()
		{
			var items = await _unitOfWork.CoolingSystems.GetAllAsync();
			return _mapper.Map<IEnumerable<CoolingSystemDTO>>(items);
		}

		public async Task<CoolingSystemDTO?> GetByIdAsync(int id)
		{
			var item = await _unitOfWork.CoolingSystems.GetByIdAsync(id);
			if (item == null || item.IsDeleted) return null;

			return _mapper.Map<CoolingSystemDTO>(item);
		}

		public async Task<bool> UpdateAsync(int id, CoolingSystemUpdateDTO dto)
		{
			var entity = await _unitOfWork.CoolingSystems.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				throw new NotFoundException("Cooling system not found");
			_mapper.Map(dto, entity);
			entity.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.CoolingSystems.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
