namespace Repository.Repository
{
	public class ChipModelRepository : BaseRepository<ChipModel>,IChipModelRepository
	{
		public ChipModelRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
