namespace Service.Services
{
	public class BrandService : IBrandService
	{
		private readonly IBrandRepository _repository;
		private readonly IMapper _mapper;
		public BrandService(IBrandRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		// add brand
		public async Task<BrandDTO> AddBrandAsync(BrandCreateDTO createBrandDTO)
		{
			if (string.IsNullOrWhiteSpace(createBrandDTO.BrandName))
				throw new ArgumentException("Brand name is required.");
			if (await BrandNameExistAsync(createBrandDTO.BrandName))
				throw new InvalidOperationException("Brand name already exists.");
			var brandEntity = _mapper.Map<Brand>(createBrandDTO);
			var createdBrand = await _repository.AddAsync(brandEntity);
			if (createdBrand == null)
				throw new InvalidOperationException("Failed to create brand.");
			return _mapper.Map<BrandDTO>(createdBrand);
		}
		// Check if brand name exists
		public async Task<bool> BrandNameExistAsync(string brandName)
		{
			if(string.IsNullOrWhiteSpace(brandName))
				throw new ArgumentException("Brand name cannot be null or empty.");
			return await _repository.BrandNameExistAsync(brandName);
		}
		// Get all brands
		public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
		{
			var brands = await _repository.GetAllAsync();
			if (brands == null || !brands.Any())
			{
				return new List<BrandDTO>();
			}
			var brandsDTO = _mapper.Map<IEnumerable<BrandDTO>>(brands);
			return brandsDTO;
		}
		// Get brand by id
		public async Task<BrandDTO?> GetBrandByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var brand = await _repository.GetByIdAsync(id);
			if (brand == null)
			{
				throw new NotFoundException($"Brand with ID {id} not found.");
			}
			return _mapper.Map<BrandDTO>(brand);
		}
		//update brand
		public async Task<BrandDTO?> UpdateBrandAsync(int id, BrandUpdateDTO updateBrandDTO)
		{
			var brand = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Brand with ID {id} not found.");

			if (updateBrandDTO == null)
			{
				throw new ArgumentNullException(nameof(updateBrandDTO), "Updated brand cannot be null.");
			}
			_mapper.Map(updateBrandDTO, brand);
			brand.UpdatedAt = DateTime.Now; // Update the timestamp
			var updatedBrand = await _repository.UpdateAsync(id, brand);
			return _mapper.Map<BrandDTO>(updatedBrand);

		}
	}
}
