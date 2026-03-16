using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;
using Service.IServices.AdminManagement;
using System;

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
		public async Task<UserDTO> RegisterAdminAsync(adminCreateDTO createAdminDTO)
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
			//check username
			var existsUserName = await _unitOfWork.Users.UserNameExistsAsync(createAdminDTO.Username);
			if (existsUserName)
			{
				throw new InvalidOperationException("Username already exists.");
			}
			// Xử lý logic Role để tránh nhầm lẫn Customer
			var role = await _unitOfWork.Roles.GetByIdAsync(createAdminDTO.RoleId);

			// Cách 1: Dùng string.Equals (Static method) - Cực kỳ an toàn
			if (role == null || string.Equals(role.RoleName.ToString(), "User", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Không thể cấp quyền Khách hàng trong luồng quản trị.");
			}
			//DTO-> Entity
			var userEntity = _mapper.Map<User>(createAdminDTO);
			// Encryption password
			userEntity.HashPassword = _passwordHasher.HashPassword(userEntity, createAdminDTO.Password);
			//lưu db
			var result = await _unitOfWork.Users.AddAsync(userEntity);
			// 6. Ghi Audit Log (Vì trong UnitOfWork bạn đã có AuditLogs)
			 await _unitOfWork.AuditLogs.AddAsync(new AuditLog { Action = Enum.EnumableClass.EnumableClass.EActionLog.Insert });
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(result);
		}
	}
}
