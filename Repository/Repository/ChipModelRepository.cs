namespace Repository.Repository
{
	public class ChipModelRepository : BaseRepository<ChipModel>,IChipModelRepository
	{
		public ChipModelRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
