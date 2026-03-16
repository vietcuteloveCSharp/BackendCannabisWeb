using Service.IServices.Product;

namespace Service.Services.Product
{
	public class NutrientService : INutrientService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public NutrientService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		// Create a new nutrient
		public async Task<NutrientDTO> CreateAsync(NutrientCreateDTO dto)
		{
			// check if Quantity is negative or zero
			ArgumentOutOfRangeException.ThrowIfNegative(dto.Quantity, nameof(dto.Quantity));
			var nutrient = _mapper.Map<Nutrient>(dto);
			await _unitOfWork.Nutrients.AddAsync(nutrient);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<NutrientDTO>(nutrient);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.Nutrients.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.Nutrients.Update(entity);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public async Task<bool> ExistAsync(int id)
		{
			var item = await _unitOfWork.Nutrients.GetByIdAsync(id);
			return item != null && !item.IsDeleted;
		}

		public async Task<IEnumerable<NutrientDTO?>> GetAllActiveAsync()
		{
			var nutrients = await _unitOfWork.Nutrients.GetAllActiveAsync();
			if (nutrients == null || !nutrients.Any())
			{
				return new List<NutrientDTO>();
			}
			return _mapper.Map<IEnumerable<NutrientDTO?>>(nutrients);
		}

		// Get all nutrients
		public async Task<IEnumerable<NutrientDTO?>> GetAllAsync()
		{
			var nutrients = await _unitOfWork.Nutrients.GetAllAsync();
			if (nutrients == null || !nutrients.Any())
			{
				return new List<NutrientDTO>();
			}
			return _mapper.Map<IEnumerable<NutrientDTO?>>(nutrients);
		}
		// Get nutrient by ID
		public async Task<NutrientDTO?> GetByIdAsync(int id)
		{
			var nutrient = await _unitOfWork.Nutrients.GetByIdAsync(id);
			if (nutrient == null)
			{
				throw new NotFoundException($"Nutrient with ID {id} not found.");
			}
			return _mapper.Map<NutrientDTO?>(nutrient);
		}
		// Update nutrient
		public async Task<bool> UpdateAsync(int id, NutrientUpdateDTO dto)
		{
			
			ArgumentOutOfRangeException.ThrowIfNegative(dto.Quantity, nameof(dto.Quantity));

			var nutrient = await _unitOfWork.Nutrients.GetByIdAsync(id);
			if (nutrient == null)
			{
				throw new NotFoundException($"Nutrient with ID {id} not found.");
			}
			_mapper.Map(dto, nutrient);
			nutrient.UpdatedAt = DateTime.Now;
			_unitOfWork.Nutrients.Update(nutrient);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
