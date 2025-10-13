namespace Service.IServices
{
	public interface IBrandService 
	{
		Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
		Task<BrandDTO> AddBrandAsync(BrandCreateDTO createBrandDTO);
		Task<BrandDTO?> GetBrandByIdAsync(int id);
		Task<BrandDTO?> UpdateBrandAsync( int id, BrandUpdateDTO updateBrandDTO );
		Task<bool> BrandNameExistAsync(string brandName);
		
	}
}
