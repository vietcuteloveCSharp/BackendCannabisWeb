using DTO.DTOs.Admin.Admins;
using DTO.DTOs.User.Users;
using DTO.Response;
using Enum.Domain;
using Microsoft.EntityFrameworkCore;
using Service.IServices.AdminManagement;
using System;
using static Enum.Domain.System_User;

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
		//get all user có kèm lọc 
		public async Task<PagedResult<UserDTO>> GetAllUsersAsync(UserFilterDTO filter)
		{
			// Lấy query từ Repository (Chưa thực thi xuống DB)
			var query = _unitOfWork.Users.GetQueryable();
			// Lọc theo search term (Name, Username, Email)
			if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
			{
				var search = filter.SearchTerm.ToLower();
				query = query.Where(u => u.Username!.ToLower().Contains(search)
									  || u.Name!.ToLower().Contains(search)
									  || u.Email!.ToLower().Contains(search));
			}
			// Lọc theo RoleId và Status
			if (filter.RoleId.HasValue)
				query = query.Where(u => u.RoleId == filter.RoleId.Value);

			if (filter.Status.HasValue)
				query = query.Where(u => u.Status == filter.Status.Value);

			// Đếm tổng số bản ghi trước khi phân trang
			var totalItems = await query.CountAsync();

			// Thực hiện phân trang và Include Role để tránh null RoleName
			var items = await query
				.Include(u => u.Role)
				.OrderByDescending(u => u.UserId)
				.Skip((filter.PageNumber - 1) * filter.PageSize)
				.Take(filter.PageSize)
				.ToListAsync();

			return new PagedResult<UserDTO>
			{
				Items = _mapper.Map<IEnumerable<UserDTO>>(items),
				TotalItems = totalItems,
				CurrentPage = filter.PageNumber,
				PageSize = filter.PageSize
			};
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
			userEntity.HashPassword = _passwordHasher.HashPassword(userEntity, createAdminDTO.Password);
			//lưu db
			var result = await _unitOfWork.Users.AddAsync(userEntity);
			// 6. Ghi Audit Log (Vì trong UnitOfWork bạn đã có AuditLogs)
			 await _unitOfWork.AuditLogs.AddAsync(new AuditLog { Action = "Đăng kí admin",
				 UserId = userEntity.UserId, // Giờ đã có ID sau khi SaveChanges
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

			user.Status = statusDTO.Status; //

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
