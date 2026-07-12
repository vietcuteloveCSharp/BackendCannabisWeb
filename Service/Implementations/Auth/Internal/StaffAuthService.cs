using RefreshTokenRequest = Shared.DTOs.DTO.Internal.RefreshTokenRequest;
using LoginRequest = Shared.DTOs.DTO.Internal.LoginRequest;
using LogoutRequest = Shared.DTOs.DTO.Internal.LogoutRequest;
using TokenRotationRequest = Shared.DTOs.DTO.Internal.TokenRotationRequest;
namespace Service.Implementations.Auth.Internal
{
	public class StaffAuthService :IStaffAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITokenService _tokenService;
		private readonly IStaffRefreshTokenService _staffRefreshTokenService;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<Staff> _passwordHasher;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly JwtSettings _jwtSettings;
		public StaffAuthService(
			IUnitOfWork unitOfWork,
			ITokenService tokenService,
			IStaffRefreshTokenService staffRefreshTokenService,
			IMapper mapper,
			IPasswordHasher<Staff> passwordHasher,
			IOptions<JwtSettings> jwtSettings,
			IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
			_staffRefreshTokenService = staffRefreshTokenService ?? throw new ArgumentNullException(nameof(staffRefreshTokenService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}
		public async Task<TokenResponse> LoginAsync(LoginRequest request)
		{
			// 1. Tìm tài khoản nhân viên kèm thông tin phân quyền nội bộ
			var staff = await _unitOfWork.Staffs.GetByUsernameAsync(request.Username);
			if (staff == null)
			{
				throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác.");
			}

			// 2. Kiểm tra mật khẩu băm
			var result = _passwordHasher.VerifyHashedPassword(staff, staff.PasswordHash!, request.Password);
			if (result != PasswordVerificationResult.Success)
			{
				throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác.");
			}

			// 3. Kiểm tra trạng thái hoạt động nội bộ (Bắt buộc phải thuộc nhóm Active)
			if (staff.Status != null && !string.Equals(staff.Status.Name, "Active", StringComparison.OrdinalIgnoreCase))
			{
				throw new UnauthorizedAccessException("Tài khoản hiện đang bị khóa hoặc chưa kích hoạt.");
			}

			// 4. Cấu hình danh sách Claims chuẩn mã hóa JWT phục vụ phân quyền hệ thống Admin
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, staff.Id.ToString()),
				new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, staff.Role != null ? staff.Role.RoleName : "Staff"),
				new Claim(ClaimTypes.Name, staff.Username!),
				new Claim(JwtRegisteredClaimNames.Email, staff.Email!),
				new Claim("isAdmin", (staff.Role != null && string.Equals(staff.Role.RoleName, "Admin", StringComparison.OrdinalIgnoreCase)).ToString().ToLower())
			};

			// 5. Sinh mã Access Token từ TokenService chung của hệ thống
			var accessToken = _tokenService.GenerateAccessToken(claims);

			// 6. Gọi StaffRefreshTokenService tạo mới token lưu tạm vào Tracker
			var refreshTokenEntity = await _staffRefreshTokenService.GenerateRefreshTokenAsync(staff.Id);

			// 7. Tạo vết thiết bị và lưu phiên làm việc (Session)
			var httpContext = _httpContextAccessor.HttpContext;
			if (httpContext != null)
			{
				string ipAddress = httpContext.Request.Headers["X-Forwarded-For"].ToString();
				if (string.IsNullOrEmpty(ipAddress))
				{
					ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
				}

				string userAgent = httpContext.Request.Headers["User-Agent"].ToString() ?? "Unknown";

				var staffSession = new StaffSession
				{
					StaffId = staff.Id,
					SessionToken = Guid.NewGuid().ToString(),
					IpAddress = ipAddress,
					UserAgent = userAgent,
					LoginAt = DateTime.UtcNow,
					ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
				};
				await _unitOfWork.StaffSessions.AddAsync(staffSession);
			}

			// Cập nhật mốc thời gian truy cập
			staff.LastLoginAt = DateTime.UtcNow;
			_unitOfWork.Staffs.Update(staff);

			// Gom lại thực thi SaveChanges 1 lần duy nhất để tối ưu hiệu năng DB
			await _unitOfWork.SaveChangesAsync();

			return new TokenResponse
			{
				AccessToken = accessToken,
				RefreshToken = refreshTokenEntity.TokenHash,
				ExpiresInSeconds = _jwtSettings.AccessTokenLifetimeMinutes * 60,
				Staff = _mapper.Map<StaffSummaryDTO>(staff)
			};
		}

		public async Task LogoutAsync(LogoutRequest request)
		{
			// Hủy kích hoạt Refresh Token thông qua tầng service cô lập
			await _staffRefreshTokenService.RevokeTokenAsync(request.RefreshTokenValue);

			// Tìm và hủy toàn bộ các phiên làm việc đang hoạt động của nhân viên này
			var activeSessions = await _unitOfWork.StaffSessions
				.GetAllAsync(s => s.StaffId == request.StaffId && s.IsDeleted == false);

			foreach (var session in activeSessions)
			{
				session.IsDeleted = true;
				session.DeletedAt = DateTime.UtcNow;
				session.DeletedBy = request.StaffId;
				_unitOfWork.StaffSessions.Update(session);
			}

			await _unitOfWork.SaveChangesAsync();
		}

		

		public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
		{
			// 1. Trích xuất Claims từ Access Token đã quá hạn thông qua TokenService
			var principal = _tokenService.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);
			if (principal == null)
			{
				throw new UnauthorizedAccessException("Access Token không hợp lệ hoặc không đúng định dạng.");
			}

			var staffIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst(JwtRegisteredClaimNames.Sub);
			if (staffIdClaim == null || !int.TryParse(staffIdClaim.Value, out int staffId))
			{
				throw new UnauthorizedAccessException("Token thiếu thông tin định danh hợp lệ.");
			}

			// 2. Tạo Request xoay vòng Token hướng Object chuyển cho Service xử lý dưới DB
			var rotationRequest = new TokenRotationRequest
			{
				StaffId = staffId,
				OldRefreshToken = request.OldRefreshToken
			};
			var newRefreshTokenEntity = await _staffRefreshTokenService.ReplaceRefreshTokenAsync(rotationRequest);

			// 3. Tải lại thực thể nhân viên để làm mới bộ Claims (Cập nhật quyền hạn thời gian thực)
			var staff = await _unitOfWork.Staffs.GetByIdAsync(staffId);
			if (staff == null || staff.Status != null && !string.Equals(staff.Status.Name, "Active", StringComparison.OrdinalIgnoreCase))
			{
				throw new UnauthorizedAccessException("Tài khoản nhân viên không tồn tại hoặc đã bị khóa.");
			}

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, staff.Id.ToString()),
				new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, staff.Role != null ? staff.Role.RoleName : "Staff"),
				new Claim(ClaimTypes.Name, staff.Username!),
				new Claim(JwtRegisteredClaimNames.Email, staff.Email!),
				new Claim("isAdmin", (staff.Role != null && string.Equals(staff.Role.RoleName, "Admin", StringComparison.OrdinalIgnoreCase)).ToString().ToLower())
			};

			// 4. Ký mã cấp Access Token mới tinh
			var newAccessToken = _tokenService.GenerateAccessToken(claims);

			await _unitOfWork.SaveChangesAsync();

			return new TokenResponse
			{
				AccessToken = newAccessToken,
				RefreshToken = newRefreshTokenEntity.TokenHash,
				ExpiresInSeconds = _jwtSettings.AccessTokenLifetimeMinutes * 60,
				Staff = _mapper.Map<StaffSummaryDTO>(staff)
			};
		}

		
	}
}
