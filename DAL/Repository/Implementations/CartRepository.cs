
namespace DAL.Repository.Implementations
{
	public class CartRepository : BaseRepository<Cart>
	{
		public CartRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
