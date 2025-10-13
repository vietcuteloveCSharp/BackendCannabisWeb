

namespace Repository.Repository
{
	public class PowerSupplyRepository : BaseRepository<PowerSupply>,IPowerSupplyRepository
	{
		
		public PowerSupplyRepository(CannabisAccessorriesDBContext context) :base(context)
		{
			
		}

		
	}
}
