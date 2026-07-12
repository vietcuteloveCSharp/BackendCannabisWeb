namespace DAL.Repository.Implementations.Shop
{
	public class BrandRepository : BaseRepository<Brand>,IBrandRepository
	{
	
		public BrandRepository(CannabisAccessoriesDBContext context) : base(context)
		{
			
		}		
		// check if brand name exists
		public async Task<bool> BrandNameExistAsync(string brandName)
		{
			var BrandNameExists = await _context.Brands.AnyAsync(b => b.BrandName == brandName);
			return BrandNameExists;
		}

		public async Task<bool> ExistsAsync(int brandId)
		{
			return await _context.Brands.AnyAsync(b => b.Id == brandId);
		}
	}
}
