using Service.IServices.Product;

namespace Service.Services.Product
{
	public class CategoryService : ICategoryService
	{	private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
		{ 
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		// Add new category
		public async Task<CategoryDTO> AddAsync(CategoryCreateDTO createCategoryDTO)
		{
			ArgumentNullException.ThrowIfNull(createCategoryDTO, nameof(createCategoryDTO));

			var category = _mapper.Map<Category>(createCategoryDTO);
			await _unitOfWork.Categories.AddAsync(category);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<CategoryDTO>(category);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.Categories.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.Categories.Update(entity);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<CategoryDTO?>> GetAllActiveAsync()
		{
			var categories = await _unitOfWork.Categories.GetAllActiveAsync();
			if (categories == null) return new List<CategoryDTO>();
			return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
		}

		// Get all categories
		public async Task<IEnumerable<CategoryDTO?>> GetAllAsync()
		{
			var categories = await _unitOfWork.Categories.GetAllAsync();
			if (categories == null) return new List<CategoryDTO>();
			return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
		}

		public async Task<CategoryDTO?> GetByIdAsync(int id)
		{
			var category = await _unitOfWork.Categories.GetByIdAsync(id);
			if (category == null) return null;

			return _mapper.Map<CategoryDTO>(category);
		}

		public async Task<CategoryDTO?> GetByNameAsync(string name)
		{
			var category = await _unitOfWork.Categories
			   .FindAsync(c => c.CategoryName.ToLower() == name.ToLower());

			return category == null ? throw new NotFoundException($"Category with name {name} not found") : _mapper.Map<CategoryDTO>(category);
		}

		public async Task<bool> UpdateAsync(int id,CategoryUpdateDTO updateCategoryDTO)
		{
			var category = await _unitOfWork.Categories.GetByIdAsync(id);
			if (category == null) throw new NotFoundException($"Category with id {id} not found");
			// Map DTO -> entity
			_mapper.Map(updateCategoryDTO, category);

			// Update entity
			_unitOfWork.Categories.Update(category);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
	}
}
