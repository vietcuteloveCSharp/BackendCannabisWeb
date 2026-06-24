namespace DAL.Repository.Interfaces
{
	public interface IBrandRepository : IBaseRepository<Brand>
	{
		Task<bool> BrandNameExistAsync(string brandName);
		Task<bool> ExistsAsync(int brandId);
	}
}
