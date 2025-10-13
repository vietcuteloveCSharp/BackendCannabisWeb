namespace Repository.Repository
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
