using DTO.DTOs.User.Users;
using DTO.Request;

namespace Service.Services.ServicesAuth
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITokenService _tokenService;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<User> _passwordHasher;
		
		private readonly JwtSettings _jwtSettings;
		public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IRefreshTokenService refreshTokenService, IMapper mapper, IPasswordHasher<User> passwordHasher,IOptions<JwtSettings> jwtSettings)
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
			var user = await _unitOfWork.Users.GetByUsernameAsync(loginResquestDTO.Username);
			if (user == null)
			{
				throw new UnauthorizedAccessException($"Invalid account");
			}
			var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword!, loginResquestDTO.Password);
			if (result == PasswordVerificationResult.Failed)
				throw new UnauthorizedAccessException("Invalid password.");

			// 1. Tạo payload cho token
			var payload = new TokenPayload
			{
				UserId = user.UserId.ToString(),
				UserName = user.Username!,
				Role = user.Role!.RoleName.ToString(),
				Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifetimeMinutes)
			};
			var accessToken = _tokenService.GenerateAccessToken(payload);
			string? refreshTokenValue = null;
			var tokenDTO = new TokenDTO
			{
				AccessToken = accessToken,
				Expiration = payload.Expiration,
				User = _mapper.Map<UserSummaryDTO>(user)
			};
			if (loginResquestDTO.RememberMe)
			{
				var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.UserId);
				refreshTokenValue =  refreshToken.RefreshTokenValue;

				var refreshTokenDTO = new RefreshTokenDTO
				{
					UserId = user.UserId,
					RefreshTokenValue = refreshTokenValue,
					IsRevoked = false,
					ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetimeDays)
				};
				await _refreshTokenService.StoreTokenAsync(refreshTokenDTO);
				tokenDTO.RefreshToken = refreshTokenValue;
			}
			return tokenDTO;

		}

		public async Task LogoutAsync(int userId, string refreshTokenValue)
		{
			if (string.IsNullOrEmpty(refreshTokenValue))
				throw new ArgumentNullException(nameof(refreshTokenValue), "Refresh token value is required.");
			// 1. Lấy token từ DB
			var token = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshTokenValue);
			// 2. Kiểm tra token có tồn tại & thuộc về user này không
			if (token == null || token.UserId != userId)
				throw new UnauthorizedAccessException("Invalid or expired refresh token");
			if (token.UserId != userId)
				throw new UnauthorizedAccessException("Token does not belong to this user.");
			if (token.IsRevoked)
				return;
			// 3. Đánh dấu token là đã revoke
			token.IsRevoked = true;
			await _unitOfWork.RefreshTokens.UpdateAsync(token);
			await _unitOfWork.SaveChangesAsync();
		}

	}
}
