namespace Repository.Repository
{
	public class AddressRepository : BaseRepository<Address>,IAddressRepository
	{
		public AddressRepository(CannabisAccessorriesDBContext context) : base(context)
		{
		}
	}
}
