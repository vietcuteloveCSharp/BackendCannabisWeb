using DAL.Repository.Interfaces.Shop;

namespace DAL.Repository.Implementations.Shop
{
	public class AddressRepository : BaseRepository<Address>,IAddressRepository
	{
		public AddressRepository(CannabisAccessoriesDBContext context) : base(context)
		{
		}
	}
}
