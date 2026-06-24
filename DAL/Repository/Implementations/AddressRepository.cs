namespace DAL.Repository.Implementations
{
	public class AddressRepository : BaseRepository<Address>,IAddressRepository
	{
		public AddressRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
