namespace Repository.Repository
{
	public class OrderRepository : BaseRepository<Order>
	{
		public OrderRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
