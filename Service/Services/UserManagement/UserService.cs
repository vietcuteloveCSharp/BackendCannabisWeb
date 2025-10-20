using DTO.DTOs.User.Users;
using Service.IServices.UserManagement;

namespace Service.Services.UserManagement
{
	public class UserService:IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<User> _passwordHasher;
		public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher)
		{
			_unitOfWork = unitOfWork;	
			_mapper = mapper;
			_passwordHasher = passwordHasher;
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
			ArgumentException.ThrowIfNullOrWhiteSpace(nameof(userDto));
			// Map sang entity
			var entity = _mapper.Map<User>(userDto);
			entity.UserId = id;
			entity.UpdatedAt = DateTime.UtcNow;
			// Gọi repository update
			var updated = await _unitOfWork.Users.UpdateAsync(id, entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(updated);

		}
		// Register a new user
		public async Task<UserDTO> RegisterUserAsync(CreateUserDTO createUserDTO)
		{   //check null input

			ArgumentNullException.ThrowIfNull(createUserDTO, nameof(createUserDTO));
			ArgumentNullException.ThrowIfNull(createUserDTO.Password, nameof(createUserDTO.Password));
			//check mail
			var existsEmail = await _unitOfWork.Users.EmailExistsAsync(createUserDTO.Email);
			if (existsEmail)
			{
				throw new InvalidOperationException("Email already exists.");
			}
			var existsUserName = await _unitOfWork.Users.UserNameExistsAsync(createUserDTO.Username);
			if (existsUserName)
			{
				throw new InvalidOperationException("Username already exists.");
			}
			var userRole = await _unitOfWork.Roles.GetByNameAsync("User");
			if (userRole == null)
			{
				throw new InvalidOperationException("Role 'User' chưa tồn tại trong DB");
			}
			//DTO-> Entity
			var userEntity = _mapper.Map<User>(createUserDTO);
			// Encryption password
			userEntity.HashPassword = _passwordHasher.HashPassword(userEntity, createUserDTO.Password);
			//gán role mặc định user
			userEntity.RoleId = userRole.RoleId;
			var result = await _unitOfWork.Users.AddAsync(userEntity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(result);
		}
	}
}
