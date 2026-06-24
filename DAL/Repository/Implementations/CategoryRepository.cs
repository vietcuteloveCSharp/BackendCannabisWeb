namespace DAL.Repository.Implementations
{
	public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
