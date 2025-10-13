
namespace Repository.Repository
{
	public class CartRepository : BaseRepository<Cart>
	{
		public CartRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
