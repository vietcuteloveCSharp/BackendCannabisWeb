namespace DAL.Repository.Implementations
{
	internal class ProductImageRepository : BaseRepository<ProductImage>
	{
		public ProductImageRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
