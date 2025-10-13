namespace Repository.Repository
{
	public class BreederRepository : BaseRepository<Breeder>,IBreederRepository
	{
		public BreederRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
