namespace Repository.Repository
{
	public class AddressRepository : BaseRepository<Address>,IAddressRepository
	{
		public AddressRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
