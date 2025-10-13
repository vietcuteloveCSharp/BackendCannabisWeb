
namespace Repository.Repository
{
	public class CoolingSystemRepository :BaseRepository<CoolingSystem>, ICoolingSystemRepository
	{
		public CoolingSystemRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		
		}
		
	}
	
	
}
