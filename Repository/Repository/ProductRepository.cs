
namespace Repository.Repository
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}

		
	}
}
