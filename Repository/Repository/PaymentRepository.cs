namespace Repository.Repository
{
	public class PaymentRepository : BaseRepository<Payment>
	{
		public PaymentRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
