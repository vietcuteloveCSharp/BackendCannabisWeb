namespace Repository.Repository
{
	public class SeedRepository : BaseRepository<Seed>, ISeedRepository
	{
		public SeedRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
