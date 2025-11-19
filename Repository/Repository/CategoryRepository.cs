
namespace Repository.Repository
{
	public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
