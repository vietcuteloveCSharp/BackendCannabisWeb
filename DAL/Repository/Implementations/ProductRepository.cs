namespace DAL.Repository.Implementations
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}

		
	}
}
