namespace DAL.Repository.Implementations
{
	public class CartItemRepository : BaseRepository<CartItem>
	{
		public CartItemRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
