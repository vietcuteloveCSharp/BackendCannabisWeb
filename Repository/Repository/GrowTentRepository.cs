namespace Repository.Repository
{
	public class GrowTentRepository :BaseRepository<GrowTent>, IGrowTentRepository
	{
		public GrowTentRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
	

}
