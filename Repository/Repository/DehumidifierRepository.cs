namespace Repository.Repository
{
	public class DehumidifierRepository : BaseRepository<Dehumidifier>,IDehumidifierRepository
	{
		public DehumidifierRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
