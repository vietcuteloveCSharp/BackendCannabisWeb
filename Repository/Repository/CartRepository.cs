
namespace Repository.Repository
{
	public class CartRepository : BaseRepository<Cart>
	{
		public CartRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
