namespace Repository.Repository
{
	public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
