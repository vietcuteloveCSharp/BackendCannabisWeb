namespace Repository.Repository
{
	public class BreederRepository : BaseRepository<Breeder>,IBreederRepository
	{
		public BreederRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
