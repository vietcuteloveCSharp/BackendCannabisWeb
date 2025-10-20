
using Microsoft.EntityFrameworkCore;

namespace Service.Services.ServicesAuth
{
	public class RefreshTokenService : IRefreshTokenService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly JwtSettings _jwtSettings;
		public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService, IOptions<JwtSettings> jwtSettings)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Unit of work cannot be null.");
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null.");
			_tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService), "Token service cannot be null.");
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings), "JWT settings cannot be null.");
		}

		public async Task RevokeTokensByUserIdAsync(int userId)
		{
			var tokens = await _unitOfWork.RefreshTokens
			.GetByUserIdAsync(userId, onlyActive: true);

			if (tokens.Any())
			{
				foreach (var token in tokens)
					token.IsRevoked = true;

				foreach (var token in tokens)
					await _unitOfWork.RefreshTokens.UpdateAsync(token);

				await _unitOfWork.SaveChangesAsync();
			}
		}

		public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
		{
			// Tạo chuỗi refresh token ngẫu nhiên, bảo mật
			var randomBytes = new byte[64];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomBytes);
			}
			string refreshTokenValue = Convert.ToBase64String(randomBytes);
			// Tạo object RefreshToken
			var refreshToken = new RefreshToken
			{
				UserId = userId,
				RefreshTokenValue = refreshTokenValue,
				CreatedAt = DateTime.UtcNow,
				ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetimeDays), // thời gian sống 30 ngày, có thể config
				IsRevoked = false
			};
			await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
			await _unitOfWork.SaveChangesAsync();
			return refreshToken;
		}

		public async Task<RefreshTokenDTO?> GetTokenAsync(string refreshTokenValue)
		{
			// Lấy token từ DB
			var token = await _unitOfWork.RefreshTokens
				.GetByTokenAsync(refreshTokenValue);
			if (token == null || token.IsRevoked || token.ExpiresAt <= DateTime.UtcNow)
			{

				throw new UnauthorizedAccessException("Invalid refresh token");
			}
			// Map entity sang DTO
			var tokenDto = _mapper.Map<RefreshTokenDTO>(token);
			return tokenDto;
		}

		public async Task<RefreshToken> ReplaceRefreshTokenAsync(int userId, string oldRefreshTokenValue)
		{
			// Lấy token cũ
			var oldToken = await _unitOfWork.RefreshTokens
											.GetByTokenAsync(oldRefreshTokenValue, includeRevoked: true);

			if (oldToken == null || oldToken.UserId != userId)
				throw new UnauthorizedAccessException("Invalid refresh token");

			// Revoke token cũ
			oldToken.IsRevoked = true;
			await _unitOfWork.RefreshTokens.UpdateAsync(oldToken);

			// Tạo token mới bằng cách gọi GenerateRefreshTokenAsync
			var newToken = await GenerateRefreshTokenAsync(userId);

			return newToken;
		}

		public async Task RevokeAllAsync(int userId)
		{
			var tokens = await _unitOfWork.RefreshTokens.GetByUserIdAsync(userId, onlyActive: true);

			foreach (var token in tokens)
				token.IsRevoked = true;

			await _unitOfWork.SaveChangesAsync();
		}

		public async Task RevokeTokenAsync(string refreshTokenValue)
		{
			var token = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshTokenValue, includeRevoked: true);

			if (token == null)
				return;

			token.IsRevoked = true;
			await _unitOfWork.RefreshTokens.UpdateAsync(token);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task StoreTokenAsync(RefreshTokenDTO refreshTokenDTO)
		{
			ArgumentNullException.ThrowIfNull(refreshTokenDTO, nameof(refreshTokenDTO));
			var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDTO);
			await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> ValidateRefreshTokenAsync(string refreshTokenValue)
		{
			if (string.IsNullOrWhiteSpace(refreshTokenValue)) return false;

			// Lấy token từ repository, bao gồm cả token đã bị revoke để kiểm tra
			var token = await _unitOfWork.RefreshTokens
										  .GetByTokenAsync(refreshTokenValue, includeRevoked: true);

			// Kiểm tra token tồn tại, chưa bị revoke và chưa hết hạn
			if (token == null || token.IsRevoked || token.ExpiresAt <= DateTime.UtcNow)
				return false;

			return true;
		}
	}
}
