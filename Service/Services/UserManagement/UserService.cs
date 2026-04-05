using DAL.Entities.User;
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
			var user = _unitOfWork.Users.GetByIdAsync(id);
			if (user == null) throw new KeyNotFoundException($"User with id{id} not found");
			// Map sang entity
			var entity = _mapper.Map<User>(userDto);
			entity.UpdatedAt = DateTime.UtcNow;
			// Gọi repository update
			var updated = _unitOfWork.Users.Update(entity);
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
			userEntity.PasswordHash = _passwordHasher.HashPassword(userEntity, createUserDTO.Password);
			//gán role mặc định user
			userEntity.RoleId = userRole.Id;
			var result = await _unitOfWork.Users.AddAsync(userEntity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(result);
		}

		public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(userId);
			if (user == null) throw new NotFoundException("User not found.");

			// 1. Kiểm tra mật khẩu cũ có đúng không
			var verifyOldPass = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, changePasswordDto.OldPassword);
			if (verifyOldPass == PasswordVerificationResult.Failed)
			{
				throw new InvalidOperationException("Mật khẩu cũ không chính xác.");
			}

			// 2. Hash mật khẩu mới và cập nhật
			user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);

			_unitOfWork.Users.Update(user);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
