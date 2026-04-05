using DTO.DTOs.Admin.Admins;
using Service.IServices.AdminManagement;

namespace Service.Services.AdminManagement
{
	public class AdminService : BaseService<User,UserDTO>, IAdminService 
	{
		
		private readonly IPasswordHasher<User> _passwordHasher;
		public AdminService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher) :base(unitOfWork,mapper)
		{
			
			this._passwordHasher = passwordHasher;
			
		}
		

		public async Task<UserDTO> RegisterAdminAsync(AdminCreateDTO createAdminDTO)
		{
			// 1. Validate đầu vào (Giữ nguyên của bạn)
			ArgumentNullException.ThrowIfNull(createAdminDTO);
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
			userEntity.PasswordHash = _passwordHasher.HashPassword(userEntity, createAdminDTO.Password);
			//lưu db
			var result = await _unitOfWork.Users.AddAsync(userEntity);
			// 6. Ghi Audit Log (Vì trong UnitOfWork bạn đã có AuditLogs)
			 await _unitOfWork.AuditLogs.AddAsync(new AuditLog { Action = "Đăng kí admin",
				 UserId = userEntity.Id, // Giờ đã có ID sau khi SaveChanges
				 CreatedAt = DateTime.UtcNow
			 });
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<UserDTO>(result);
		}
		// 2. Cập nhật trạng thái người dùng (Block/Active)
		public async Task<bool> UpdateUserStatusAsync(int userId, UserStatusUpdateDTO statusDTO)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(userId);
			if (user == null) throw new NotFoundException("User không tồn tại.");
			_unitOfWork.Users.Update(user);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
		// 3. Thay đổi quyền hạn (Role)
		public async Task<bool> ChangeUserRoleAsync(int userId, UserRoleUpdateDTO roleDto)
		{
			// Kiểm tra xem Role mới có tồn tại trong hệ thống không (Tránh lỗi FK)
			var roleExists = await _unitOfWork.Roles.GetByIdAsync(roleDto.NewRoleId);
			if (roleExists == null) return false;

			var user = await _unitOfWork.Users.GetByIdAsync(userId);
			if (user == null) throw new NotFoundException("User không tồn tại.");

			user.RoleId = roleDto.NewRoleId; //

			_unitOfWork.Users.Update(user);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
