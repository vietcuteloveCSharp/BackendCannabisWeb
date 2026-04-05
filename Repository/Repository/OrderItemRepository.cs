using DAL.Entities.Order;

namespace Repository.Repository
{
	public class OrderItemRepository : BaseRepository<OrderItem>
	{
		public OrderItemRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
