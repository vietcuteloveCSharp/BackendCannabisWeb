using DAL.Entities.Product;

namespace Repository.Repository
{
	internal class ProductImageRepository : BaseRepository<ProductImage>
	{
		public ProductImageRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
