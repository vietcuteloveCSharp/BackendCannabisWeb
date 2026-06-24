
using DAL.Entities.Review;
using DAL.Repository.BaseRepository;

namespace DAL.Repository.Implementations
{
	public class ReviewRepository : BaseRepository<Review>
	{
		public ReviewRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
