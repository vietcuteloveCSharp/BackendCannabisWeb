namespace Repository.Repository
{
	internal class ProductImageRepository : BaseRepository<ProductImage>
	{
		public ProductImageRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
