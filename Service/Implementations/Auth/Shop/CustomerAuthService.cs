using LoginRequest = Shared.DTOs.DTO.Shop.LoginRequest;
using LogoutRequest = Shared.DTOs.DTO.Shop.LogoutRequest;
using RefreshTokenRequest = Shared.DTOs.DTO.Shop.RefreshTokenRequest;
using TokenRotationRequest = Shared.DTOs.DTO.Shop.TokenRotationRequest;

namespace Service.Implementations.Auth.Shop
{
	public class CustomerAuthService : ICustomerAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITokenService _tokenService;
		private readonly ICustomerRefreshTokenService _customerRefreshTokenService;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<Customer> _passwordHasher;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly JwtSettings _jwtSettings;

		public CustomerAuthService(
			IUnitOfWork unitOfWork,
			ITokenService tokenService,
			ICustomerRefreshTokenService customerRefreshTokenService,
			IMapper mapper,
			IPasswordHasher<Customer> passwordHasher,
			IOptions<JwtSettings> jwtSettings,
			IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
			_customerRefreshTokenService = customerRefreshTokenService ?? throw new ArgumentNullException(nameof(customerRefreshTokenService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		public async Task<CustomerTokenResponse> LoginAsync(LoginRequest request)
		{
			// 1. Khách hàng ngoài Web có thể đăng nhập bằng Username hoặc Email
			Customer? customer = null;
			if (request.EmailOrUsername.Contains("@"))
				customer = await _unitOfWork.Customers.GetByEmailAsync(request.EmailOrUsername);
			else
				customer = await _unitOfWork.Customers.GetByUsernameAsync(request.EmailOrUsername);

			if (customer == null || _passwordHasher.VerifyHashedPassword(customer, customer.PasswordHash!, request.Password) != PasswordVerificationResult.Success)
			{
				throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác.");
			}

			// 2. Kiểm tra trạng thái tài khoản nếu cần (Ví dụ: Bị ban hoặc chưa kích hoạt)
			if (customer.IsActive == false)
			{
				throw new UnauthorizedAccessException("Tài khoản của bạn hiện đang bị khóa.");
			}

			// 3. Khởi tạo danh sách Claims cho Khách hàng ngoài Website (Phân quyền "Customer")
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
				new Claim(ClaimTypes.Role, "Customer"),
				new Claim(ClaimTypes.Name, customer.Username!),
				new Claim(JwtRegisteredClaimNames.Email, customer.Email!)
			};

			// 4. Tạo bộ đôi Access Token và Refresh Token độc lập của phân hệ Shop
			var accessToken = _tokenService.GenerateAccessToken(claims);
			var refreshTokenEntity = await _customerRefreshTokenService.GenerateRefreshTokenAsync(customer.Id);

			// 5. Ghi vết thiết bị đăng nhập / Session công khai của Website
			var httpContext = _httpContextAccessor.HttpContext;
			if (httpContext != null)
			{
				string ipAddress = httpContext.Request.Headers["X-Forwarded-For"].ToString();
				if (string.IsNullOrEmpty(ipAddress)) ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
				string userAgent = httpContext.Request.Headers["User-Agent"].ToString() ?? "Unknown";

				await _unitOfWork.CustomerSessions.AddAsync(new CustomerSession
				{
					CustomerId = customer.Id,
					SessionToken = Guid.NewGuid().ToString(),
					IpAddress = ipAddress,
					UserAgent = userAgent,
					LoginAt = DateTime.UtcNow,
					ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
				});
			}

			// Cập nhật mốc thời gian đăng nhập gần nhất
			customer.LastLoginAt = DateTime.UtcNow;
			_unitOfWork.Customers.Update(customer);

			// Commit tập trung qua UoW duy nhất 1 lần
			await _unitOfWork.SaveChangesAsync();

			return new CustomerTokenResponse
			{
				AccessToken = accessToken,
				RefreshToken = refreshTokenEntity.TokenHash,
				ExpiresInSeconds = _jwtSettings.AccessTokenLifetimeMinutes * 60,
				Customer = _mapper.Map<CustomerSummaryDTO>(customer)
			};
		}

		public async Task LogoutAsync(LogoutRequest request)
		{
			// Thu hồi token hiện tại
			await _customerRefreshTokenService.RevokeTokenAsync(request.RefreshToken);

			// Quét dọn và đóng toàn bộ session đang active của khách hàng này
			var activeSessions = await _unitOfWork.CustomerSessions.GetActiveSessionsByCustomerIdAsync(request.CustomerId);
			foreach (var session in activeSessions)
			{
				session.IsDeleted = true;
				session.DeletedAt = DateTime.UtcNow;
				_unitOfWork.CustomerSessions.Update(session);
			}

			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<CustomerTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
		{
			// 1. Trích xuất toán học lấy Principal từ token cũ hết hạn thông qua TokenService chung
			var principal = _tokenService.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);
			if (principal == null)
				throw new UnauthorizedAccessException("Access Token không hợp lệ.");

			var customerIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst(JwtRegisteredClaimNames.Sub);
			if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
				throw new UnauthorizedAccessException("Token thiếu thông tin định danh.");

			// 2. Chuyển tiếp Request xoay vòng Token hướng Object sang cho sub-service xử lý DB
			var rotationRequest = new TokenRotationRequest { CustomerId = customerId, OldRefreshToken = request.OldRefreshToken };
			var newRefreshTokenEntity = await _customerRefreshTokenService.ReplaceRefreshTokenAsync(rotationRequest);

			// 3. Nạp lại thực thể Customer kiểm tra an toàn trạng thái thời gian thực
			var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
			if (customer == null || customer.IsActive == false)
				throw new UnauthorizedAccessException("Tài khoản không hợp lệ hoặc đã bị khóa.");

			// 4. Ký cấp Access Token mới tinh
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
				new Claim(ClaimTypes.Role, "Customer"),
				new Claim(ClaimTypes.Name, customer.Username!),
				new Claim(JwtRegisteredClaimNames.Email, customer.Email!)
			};

			var newAccessToken = _tokenService.GenerateAccessToken(claims);
			await _unitOfWork.SaveChangesAsync();

			return new CustomerTokenResponse
			{
				AccessToken = newAccessToken,
				RefreshToken = newRefreshTokenEntity.TokenHash,
				ExpiresInSeconds = _jwtSettings.AccessTokenLifetimeMinutes * 60,
				Customer = _mapper.Map<CustomerSummaryDTO>(customer)
			};
		}
	}
}
