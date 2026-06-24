namespace DAL.Repository.Implementations
{
	public class OrderRepository : BaseRepository<Order>
	{
		public OrderRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
