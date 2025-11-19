using Service.IServices.Product;

namespace Service.Services.Product
{
	public class BrandService : IBrandService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		// add brand
		public async Task<BrandDTO?> AddBrandAsync(BrandCreateDTO brandCreateDTO)
		{
			// Kiểm tra tồn tại
			var isBrandExist = await _unitOfWork.Brands.BrandNameExistAsync(brandCreateDTO.BrandName);
			if (isBrandExist)
				throw new InvalidOperationException("Brand name already exists.");
			var brandEntity = _mapper.Map<Brand>(brandCreateDTO.BrandName);
			var createdBrand = await _unitOfWork.Brands.AddAsync(brandEntity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<BrandDTO>(createdBrand);
		}

		// Get all brands
		public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
		{
			var brands = await _unitOfWork.Brands.GetAllAsync();
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
			var brand = await _unitOfWork.Brands.GetByIdAsync(id);
			if (brand == null)
			{
				throw new NotFoundException($"Brand with ID {id} not found.");
			}
			return _mapper.Map<BrandDTO>(brand);
		}


		//update brand
		public async Task<bool> UpdateBrandAsync(int id, BrandUpdateDTO updateBrandDTO)
		{
			var brand = await _unitOfWork.Brands.GetByIdAsync(id) ?? throw new NotFoundException($"Brand with ID {id} not found.");

			if (updateBrandDTO == null)
			{
				throw new ArgumentNullException(nameof(updateBrandDTO), "Updated brand cannot be null.");
			}
			_mapper.Map(updateBrandDTO, brand);
			brand.UpdatedAt = DateTime.Now; // Update the timestamp
			var updatedBrand =  _unitOfWork.Brands.Update(brand);
			return true;

		}
	}
}
