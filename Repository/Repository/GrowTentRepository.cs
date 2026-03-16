namespace Repository.Repository
{
	public class GrowTentRepository :BaseRepository<GrowTent>, IGrowTentRepository
	{
		public GrowTentRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
	

}
