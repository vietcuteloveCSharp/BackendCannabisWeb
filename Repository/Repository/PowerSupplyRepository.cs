

namespace Repository.Repository
{
	public class PowerSupplyRepository : BaseRepository<PowerSupply>,IPowerSupplyRepository
	{
		
		public PowerSupplyRepository(CannabisAccessoriesDBContext context) :base(context)
		{
			
		}

		
	}
}
