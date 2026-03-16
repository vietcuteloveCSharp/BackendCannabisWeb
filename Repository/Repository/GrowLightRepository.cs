
namespace Repository.Repository
{
	public class GrowLightRepository : BaseRepository<GrowLight>, IGrowLightRepository
	{
		public GrowLightRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
