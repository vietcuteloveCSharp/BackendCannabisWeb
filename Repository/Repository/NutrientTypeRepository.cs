namespace Repository.Repository
{
	public class NutrientTypeRepository : BaseRepository<NutrientType>,INutrientTypeRepository
	{
		public NutrientTypeRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
