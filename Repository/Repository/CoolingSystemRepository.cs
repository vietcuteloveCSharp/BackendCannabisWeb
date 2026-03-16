
namespace Repository.Repository
{
	public class CoolingSystemRepository :BaseRepository<CoolingSystem>, ICoolingSystemRepository
	{
		public CoolingSystemRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		
		}
		
	}
	
	
}
