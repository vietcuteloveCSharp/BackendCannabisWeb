namespace Repository.Repository
{
	public class ClassificationRepository : BaseRepository<Classification>,IClassificationRepository
	{
		public ClassificationRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
		// Additional methods specific to Classification can be added here
	}
}
