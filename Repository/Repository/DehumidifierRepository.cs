namespace Repository.Repository
{
	public class DehumidifierRepository : BaseRepository<Dehumidifier>,IDehumidifierRepository
	{
		public DehumidifierRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
