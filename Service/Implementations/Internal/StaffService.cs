using RegisterRequest = Shared.DTOs.DTO.Internal.RegisterRequest;
using ChangePasswordRequest = Shared.DTOs.DTO.Internal.ChangePasswordRequest;
namespace Service.Implementations.Internal
{
	public class StaffService :IStaffService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<Staff> _passwordHasher;

		public StaffService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<Staff> passwordHasher)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_passwordHasher = passwordHasher;
		}

		public async Task<StaffDTO> RegisterStaffAsync(RegisterRequest request)
		{
			ArgumentNullException.ThrowIfNull(request);

			// Kiểm tra trùng lặp thông tin
			if (await _unitOfWork.Staffs.EmailExistsAsync(request.Email))
				throw new InvalidOperationException("Email already exists."); 

			if (await _unitOfWork.Staffs.UserNameExistsAsync(request.Username))
				throw new InvalidOperationException("Username already exists."); 

			if (await _unitOfWork.Staffs.GetByStaffCodeAsync(request.StaffCode) != null)
				throw new InvalidOperationException("StaffCode already exists.");

			// Xác thực Role hợp lệ nội bộ
			var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId); 
			if (role == null)
				throw new InvalidOperationException("Quyền hạn không tồn tại.");

			// Ánh xạ và mã hóa mật khẩu
			var staffEntity = _mapper.Map<Staff>(request);
			staffEntity.PasswordHash = _passwordHasher.HashPassword(staffEntity, request.Password); 			
			var result = await _unitOfWork.Staffs.AddAsync(staffEntity);
			await _unitOfWork.SaveChangesAsync(); // Đã đồng bộ sang hàm CompleteAsync của UnitOfWork mới

			return _mapper.Map<StaffDTO>(result);
		}

		public async Task<bool> ChangePasswordAsync(int staffId, ChangePasswordRequest request)
		{
			var staff = await _unitOfWork.Staffs.GetByIdAsync(staffId);
			if (staff == null) throw new KeyNotFoundException("Staff not found."); 

			var verifyOldPass = _passwordHasher.VerifyHashedPassword(staff, staff.PasswordHash!, request.OldPassword); 
			if (verifyOldPass == PasswordVerificationResult.Failed)
				throw new InvalidOperationException("Mật khẩu cũ không chính xác."); 

			staff.PasswordHash = _passwordHasher.HashPassword(staff, request.NewPassword); 
			_unitOfWork.Staffs.Update(staff);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		public async Task<bool> UpdateStaffStatusAsync(int staffId, int statusId)
		{
			var staff = await _unitOfWork.Staffs.GetByIdAsync(staffId);
			if (staff == null) throw new KeyNotFoundException("Staff không tồn tại."); 

			var statusExists = await _unitOfWork.StaffStatuses.GetByIdAsync(statusId);
			if (statusExists == null) throw new InvalidOperationException("Trạng thái không hợp lệ.");

			staff.StatusId = statusId;
			_unitOfWork.Staffs.Update(staff);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> ChangeStaffRoleAsync(int staffId, int newRoleId)
		{
			var roleExists = await _unitOfWork.Roles.GetByIdAsync(newRoleId); 
			if (roleExists == null) throw new InvalidOperationException("Quyền hạn không tồn tại."); 

			var staff = await _unitOfWork.Staffs.GetByIdAsync(staffId); 
			if (staff == null) throw new KeyNotFoundException("Staff không tồn tại."); 

			staff.RoleId = newRoleId; 
			_unitOfWork.Staffs.Update(staff); 
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}

