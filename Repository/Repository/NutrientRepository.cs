namespace Repository.Repository
{
	public class NutrientRepository : BaseRepository<Nutrient> ,INutrientRepository
	{
		public NutrientRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
