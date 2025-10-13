namespace Repository.Repository
{
	public class OrderRepository : BaseRepository<Order>
	{
		public OrderRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
