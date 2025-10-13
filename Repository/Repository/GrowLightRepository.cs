
namespace Repository.Repository
{
	public class GrowLightRepository : BaseRepository<GrowLight>, IGrowLightRepository
	{
		public GrowLightRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
