namespace Repository.Repository
{
	public class NutrientTypeRepository : BaseRepository<NutrientType>,INutrientTypeRepository
	{
		public NutrientTypeRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
