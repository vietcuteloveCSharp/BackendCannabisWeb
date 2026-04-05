namespace Repository.Repository
{
	public class CartItemRepository : BaseRepository<CartItem>
	{
		public CartItemRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
