namespace Repository.Repository
{
	public class CarbonFilterRepository : BaseRepository<CarbonFilter>,ICarbonFilterRepository
	{
		public CarbonFilterRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
