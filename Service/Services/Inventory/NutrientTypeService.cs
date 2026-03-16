using Service.IServices.Inventory;

namespace Service.Services.Inventory
{
	public class NutrientTypeService :INutrientTypeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public NutrientTypeService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		
		public async Task<NutrientTypeDTO> CreateAsync(NutrientTypeCreateDTO dto)
		{
			if (await NameExist(dto.NutrientName))
				throw new NotFoundException($"Nutrient Name already exists");
			var nutrientType = _mapper.Map<NutrientType>(dto);
			await _unitOfWork.NutrientTypes.AddAsync(nutrientType);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<NutrientTypeDTO>(nutrientType);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.NutrientTypes.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.NutrientTypes.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> ExistsAsync(int id)
		{
			var item = await _unitOfWork.NutrientTypes.GetByIdAsync(id);
			return item != null && !item.IsDeleted;
		}
		//get list of all active
		public async Task<IEnumerable<NutrientTypeDTO>> GetAllActiveAsync()
		{
			var nutrientTypes = await _unitOfWork.NutrientTypes.GetAllActiveAsync();
			if (nutrientTypes == null || !nutrientTypes.Any())
			{
				return new List<NutrientTypeDTO>();
			}
			return _mapper.Map<IEnumerable<NutrientTypeDTO>>(nutrientTypes);
		}

		//get list of all nutrient types
		public async Task<IEnumerable<NutrientTypeDTO>> GetAllAsync()
		{
			var nutrientTypes = await _unitOfWork.NutrientTypes.GetAllAsync();
			if(nutrientTypes == null || !nutrientTypes.Any())
			{
				return  new List<NutrientTypeDTO>();
			}
			return _mapper.Map<IEnumerable<NutrientTypeDTO>>(nutrientTypes);
		}
		// Get nutrient type by id
		public async Task<NutrientTypeDTO?> GetByIdAsync(int id)
		{	
			var nutrientType = await _unitOfWork.NutrientTypes.GetByIdAsync(id);
			if (nutrientType == null)
			{
				throw new NotFoundException($"Nutrient type with ID {id} not found.");
			}
			return _mapper.Map<NutrientTypeDTO>(nutrientType);
		}

		public async Task<bool> NameExist(string name)
		{
			var nutrient = await _unitOfWork.NutrientTypes.FindAsync(
				x => x.NutrientName.ToLower() == name.ToLower() && !x.IsDeleted
			);
			return nutrient != null;
		}

		//update nutrient type
		public async Task<bool> UpdateAsync(int id, NutrientTypeUpdateDTO dto)
		{
			var nutrientType = await _unitOfWork.NutrientTypes.GetByIdAsync(id);
			if (nutrientType == null) throw new NotFoundException($"Nutrient type with ID {id} not found.");
			_mapper.Map(dto, nutrientType);
			nutrientType.UpdatedAt = DateTime.UtcNow; //update time
			_unitOfWork.NutrientTypes.Update(nutrientType);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		
	}
}
