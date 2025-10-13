namespace Service.Services
{
	public class UserService:IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this._unitOfWork = unitOfWork;	
			this._mapper = mapper;
		}

		public async Task<User?> FindUserByEmailAsync(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException("Email is required.", nameof(email));
			var user = await _unitOfWork.Users.GetByEmailAsync(email);
			if (user == null)
				throw new NotFoundException($"User with email '{email}' not found.");

			return user;
		}

		// Get user by id
		public async Task<UserDTO?> GetUserByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

			var user = await _unitOfWork.Users.GetByIdAsync(id);
			if (user == null)
			{
				throw new NotFoundException($"User with ID {id} not found.");
			}
			return _mapper.Map<UserDTO>(user);
		}

		public async Task<UserDTO?> UpdateAsync(int id,UpdateUserDTO userDto)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(userDto));
			// Map sang entity
			var entity = _mapper.Map<User>(userDto);
			entity.UserId = id;
			entity.UpdatedAt = DateTime.UtcNow;
			// Gọi repository update
			var updated = await _unitOfWork.Users.UpdateAsync(id, entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(updated);

		}
	}
}
