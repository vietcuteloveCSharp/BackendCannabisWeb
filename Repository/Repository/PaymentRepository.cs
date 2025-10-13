namespace Repository.Repository
{
	public class PaymentRepository : BaseRepository<Payment>
	{
		public PaymentRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
