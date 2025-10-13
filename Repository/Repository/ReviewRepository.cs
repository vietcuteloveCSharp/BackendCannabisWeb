
namespace Repository.Repository
{
	public class ReviewRepository : BaseRepository<Review>
	{
		public ReviewRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
