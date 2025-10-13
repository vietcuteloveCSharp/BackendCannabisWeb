
namespace Repository.Repository
{
	public class CategoryRepository : BaseRepository<Category>
	{
		public CategoryRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
