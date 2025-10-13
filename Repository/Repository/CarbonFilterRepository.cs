namespace Repository.Repository
{
	public class CarbonFilterRepository : BaseRepository<CarbonFilter>,ICarbonFilterRepository
	{
		public CarbonFilterRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
