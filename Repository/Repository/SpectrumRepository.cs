namespace Repository.Repository
{
	public class SpectrumRepository : BaseRepository<Spectrum>, ISpectrumRepository
	{
		public SpectrumRepository(CannabisAccessoriesDBContext context) :base(context)
		{
			
		}
		
		
	}
}
