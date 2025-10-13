namespace Repository.Repository
{
	public class SpectrumRepository : BaseRepository<Spectrum>, ISpectrumRepository
	{
		public SpectrumRepository(CannabisAccessorriesDBContext context) :base(context)
		{
			
		}
		
		
	}
}
