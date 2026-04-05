using DAL.Entities.Product;

namespace Repository.IRepository
{
	public interface IBrandRepository : IBaseRepository<Brand>
	{
		Task<bool> BrandNameExistAsync(string brandName);
		Task<bool> ExistsAsync(int brandId);
	}
}
