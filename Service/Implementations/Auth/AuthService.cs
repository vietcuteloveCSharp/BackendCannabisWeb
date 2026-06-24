using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Implementations.Auth
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITokenService _tokenService;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<User> _passwordHasher;

		private readonly JwtSettings _jwtSettings;
		public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IRefreshTokenService refreshTokenService, IMapper mapper, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtSettings)
		{
			this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork), "User repository cannot be null.");
			this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), "Token service cannot be null.");
			this._refreshTokenService = refreshTokenService ?? throw new ArgumentNullException(nameof(refreshTokenService), "Refresh token service cannot be null.");
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null.");
			this._passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
			this._jwtSettings = jwtSettings.Value;
		}
		// Login user and generate access token and refresh token
		public async Task<TokenDTO> LoginAsync(LoginResquestDTO loginResquestDTO)
		{
			// 1. Tìm user (Nên Include luôn Role để tránh query thêm lần nữa sau này)
			var user = await _unitOfWork.Users.GetByUsernameAsync(loginResquestDTO.Username);

			// 2. Guard Clause: Nếu không có user, ném lỗi ngay
			if (user == null)
			{
				throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác.");
			}

			// 3. Kiểm tra Password
			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginResquestDTO.Password);
			if (result != PasswordVerificationResult.Success)
			{
				throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác.");
			}

			// Chuẩn bị Claims gọn nhẹ
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role!.RoleName.ToString()),
				new Claim(ClaimTypes.Name, user.Username!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			// 5. Tạo Access Token (Truyền List Claims vào)
			var accessToken = _tokenService.GenerateAccessToken(claims);

			// 6. Tạo Refresh Token
			var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id);

			// Lấy số giây từ config (3600)
			var expirationSeconds = _jwtSettings.AccessTokenLifetimeMinutes;

			var token = new TokenDTO
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken.TokenHash,
				ExpiresInSeconds = expirationSeconds, // Gán số giây vào đây
				User = _mapper.Map<UserSummaryDTO>(user)
			};
			return token;
		}

		public async Task LogoutAsync(int userId, string refreshTokenValue)
		{
			// Chúng ta dùng GetByTokenAsync để lấy đối tượng token ra check
			var tokenRecord = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshTokenValue);

			// 2. Bảo mật: Chỉ thu hồi nếu token đó đúng là của User đang yêu cầu
			if (tokenRecord != null && tokenRecord.UserId == userId)
			{
				// Sử dụng hàm có sẵn trong Repo của bạn
				await _unitOfWork.RefreshTokens.RevokeTokenAsync(refreshTokenValue);

				// Đừng quên SaveChanges nếu Repo của bạn không tự gọi nó
				await _unitOfWork.SaveChangesAsync();
			}
		}

	}
}
