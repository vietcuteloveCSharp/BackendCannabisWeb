namespace Repository.Repository
{
	public class SeedRepository : BaseRepository<Seed>, ISeedRepository
	{
		public SeedRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
