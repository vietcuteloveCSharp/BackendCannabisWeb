
using DAL.Entities.Review;

namespace Repository.Repository
{
	public class ReviewRepository : BaseRepository<Review>
	{
		public ReviewRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
