using DTO.DTOs.Addresses;

namespace Service.Services
{
	public class AddressService : IAddressService
	{
		
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public AddressService(IAddressRepository repository,IMapper mapper, IUnitOfWork unitOfWork)
		{
			
			this._mapper = mapper;
			this._unitOfWork = unitOfWork;

		}
		// create a new address
		public async Task<AddressDTO> CreateAddressAsync(int userId,AddressCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var user = _unitOfWork.Users.GetByIdAsync(userId); 
			if (user == null)
			{
				 throw new NotFoundException($"User with Id {userId} not found.");
			}
			var entity = _mapper.Map<Address>(dto);
			entity.CreatedAt = DateTime.Now;
			entity.UserId = userId;
			await _unitOfWork.Addresses.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<AddressDTO>(entity);
		}
		// get list of addresses by user id
		public async Task<IEnumerable<AddressDTO>> GetAddressByUserIdAsync(int userId)
		{
			var addresses = await _unitOfWork.Users.FindAsync(a => a.UserId == userId);
			if (addresses == null)
			{
				return new List<AddressDTO>();
			}
			return _mapper.Map<IEnumerable<AddressDTO>>(addresses);
		}
		// set default address for user
		public async Task<bool> SetDefaultAddressAsync(int userId, int addressId)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId, nameof(userId));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(addressId, nameof(addressId));
			// Lấy address mới cần đặt làm mặc định
			var target = await _unitOfWork.Addresses
				.FindAsync(a => a.UserId == userId && a.AddressId == addressId)?? throw new NotFoundException("Address not found or does not belong to the user.");
			if (target.IsDefault) return true; //đã là mặc định, không làm gì cả

			// Lấy địa chỉ hiện tại đang là mặc định (nếu có)
			var current = await  _unitOfWork.Addresses
				.FindAsync(a => a.UserId == userId && a.IsDefault);

			if (current != null)
			{
				current.IsDefault = false;
				current.UpdatedAt = DateTime.Now;
			}
			target.IsDefault = true;
			target.UpdatedAt = DateTime.Now;

			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<AddressDTO> UpdateAddressAsync(int id,AddressUpdateDTO dto)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var address = await _unitOfWork.Addresses.GetByIdAsync(id) ?? throw new NotFoundException("Address not found");
			_mapper.Map(address,dto);
			await _unitOfWork.Addresses.UpdateAsync(id,address);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<AddressDTO>(address);
		}
	}
}
