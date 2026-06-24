namespace DAL.Repository.Implementations
{
	public class OrderItemRepository : BaseRepository<OrderItem>
	{
		public OrderItemRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
