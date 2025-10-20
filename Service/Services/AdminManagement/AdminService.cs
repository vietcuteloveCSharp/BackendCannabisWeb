using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;
using Service.IServices.AdminManagement;

namespace Service.Services.AdminManagement
{
	public class AdminService : IAdminService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<User> _passwordHasher;
		public AdminService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher)
		{
			this._mapper = mapper;
			this._passwordHasher = passwordHasher;
			this._unitOfWork = unitOfWork;
		}
		public async Task<UserDTO> RegisterAdminAsync(CreateAdminDTO createAdminDTO)
		{
			//check null input

			ArgumentNullException.ThrowIfNull(createAdminDTO, nameof(createAdminDTO));
			ArgumentNullException.ThrowIfNull(createAdminDTO.Password, nameof(createAdminDTO.Password));
			//check mail
			var existsEmail = await _unitOfWork.Users.EmailExistsAsync(createAdminDTO.Email);
			if (existsEmail)
			{
				throw new InvalidOperationException("Email already exists.");
			}
			var existsUserName = await _unitOfWork.Users.UserNameExistsAsync(createAdminDTO.Username);
			if (existsUserName)
			{
				throw new InvalidOperationException("Username already exists.");
			}
			var role = await _unitOfWork.Roles.GetByIdAsync(createAdminDTO.RoleId);
			if (role == null)
				throw new InvalidOperationException("Role không hợp lệ");
			//DTO-> Entity
			var userEntity = _mapper.Map<User>(createAdminDTO);
			// Encryption password
			userEntity.HashPassword = _passwordHasher.HashPassword(userEntity, createAdminDTO.Password);

			var result = await _unitOfWork.Users.AddAsync(userEntity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(result);
		}
	}
}
