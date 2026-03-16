namespace Repository.Repository
{
	public class NutrientRepository : BaseRepository<Nutrient> ,INutrientRepository
	{
		public NutrientRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
