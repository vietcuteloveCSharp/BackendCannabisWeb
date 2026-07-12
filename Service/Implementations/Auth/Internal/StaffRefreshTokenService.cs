using TokenRotationRequest = Shared.DTOs.DTO.Internal.TokenRotationRequest;
using ValidationRequest = Shared.DTOs.DTO.Internal.ValidationRequest;

namespace Service.Implementations.Auth.Internal
{
	public class StaffRefreshTokenService :IStaffRefreshTokenService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly JwtSettings _jwtSettings;

		public StaffRefreshTokenService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
		}

		public async Task<StaffRefreshToken> GenerateRefreshTokenAsync(int staffId)
		{
			var randomBytes = new byte[64];
			using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(randomBytes); }
			string refreshTokenValue = Convert.ToBase64String(randomBytes);

			var refreshToken = new StaffRefreshToken
			{
				StaffId = staffId,
				TokenHash = refreshTokenValue,
				CreatedAt = DateTime.UtcNow,
				ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
				IsRevoked = false,
				IsUsed = false
			};

			await _unitOfWork.StaffRefreshTokens.AddAsync(refreshToken);
			return refreshToken;
		}

		public async Task<StaffRefreshToken?> GetTokenAsync(TokenQuery query)
		{
			return await _unitOfWork.StaffRefreshTokens.GetByTokenAsync(query);
		}

		public async Task<StaffRefreshToken> ReplaceRefreshTokenAsync(TokenRotationRequest request)
		{
			var query = new TokenQuery
			{
				RefreshToken = request.OldRefreshToken,
				StaffId = request.StaffId,
				IncludeRevoked = true
			};

			var oldToken = await _unitOfWork.StaffRefreshTokens.GetByTokenAsync(query);
			if (oldToken == null)
				throw new UnauthorizedAccessException("Refresh Token không hợp lệ.");

			oldToken.IsUsed = true;
			_unitOfWork.StaffRefreshTokens.Update(oldToken); // Đổi sang hàm đồng bộ chuẩn Repo mới

			return await GenerateRefreshTokenAsync(request.StaffId);
		}

		public async Task RevokeTokenAsync(string refreshTokenValue)
		{
			var query = new TokenQuery { RefreshToken = refreshTokenValue, IncludeRevoked = true };
			var token = await _unitOfWork.StaffRefreshTokens.GetByTokenAsync(query);

			if (token != null)
			{
				token.IsRevoked = true;
				_unitOfWork.StaffRefreshTokens.Update(token);
				// Xóa SaveChanges ở đây để tuân thủ đúng Unit of Work
			}
		}

		public async Task<bool> ValidateRefreshTokenAsync(ValidationRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.RefreshToken)) return false;

			var query = new TokenQuery { RefreshToken = request.RefreshToken, StaffId = request.StaffId, IncludeRevoked = false };
			var token = await _unitOfWork.StaffRefreshTokens.GetByTokenAsync(query);

			if (token == null || token.IsRevoked || token.IsUsed || token.ExpiresAt <= DateTime.UtcNow)
				return false;

			return true;
		}
	}
}
