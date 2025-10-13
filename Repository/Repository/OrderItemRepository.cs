namespace Repository.Repository
{
	public class OrderItemRepository : BaseRepository<OrderItem>
	{
		public OrderItemRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
