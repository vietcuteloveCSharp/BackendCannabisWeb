using TokenRotationRequest = Shared.DTOs.DTO.Shop.TokenRotationRequest;
using ValidationRequest = Shared.DTOs.DTO.Shop.ValidationRequest;
namespace Service.Implementations.Auth.Shop
{
	public class CustomerRefreshTokenService : ICustomerRefreshTokenService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly JwtSettings _jwtSettings;

		public CustomerRefreshTokenService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
		}

		public async Task<CustomerRefreshToken> GenerateRefreshTokenAsync(int customerId)
		{
			var randomBytes = new byte[64];
			using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(randomBytes); }
			string refreshTokenValue = Convert.ToBase64String(randomBytes);

			var refreshToken = new CustomerRefreshToken
			{
				CustomerId = customerId,
				TokenHash = refreshTokenValue,
				CreatedAt = DateTime.UtcNow,
				ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
				IsRevoked = false,
				IsUsed = false
			};

			await _unitOfWork.CustomerRefreshTokens.AddAsync(refreshToken);
			return refreshToken;
		}

		public async Task<CustomerRefreshToken?> GetTokenAsync(CustomerTokenQuery query)
		{
			return await _unitOfWork.CustomerRefreshTokens.GetByTokenAsync(query);
		}

		public async Task<CustomerRefreshToken> ReplaceRefreshTokenAsync(TokenRotationRequest request)
		{
			var query = new CustomerTokenQuery
			{
				RefreshToken = request.OldRefreshToken,
				CustomerId = request.CustomerId,
				IncludeRevoked = true
			};

			var oldToken = await _unitOfWork.CustomerRefreshTokens.GetByTokenAsync(query);
			if (oldToken == null)
				throw new UnauthorizedAccessException("Refresh Token không hợp lệ.");

			// Áp dụng cơ chế xoay vòng: Đánh dấu đã dùng và gọi hàm Update đồng bộ của Base Repo
			oldToken.IsUsed = true;
			_unitOfWork.CustomerRefreshTokens.Update(oldToken);

			return await GenerateRefreshTokenAsync(request.CustomerId);
		}

		public async Task RevokeTokenAsync(string refreshTokenValue)
		{
			// Tận dụng hàm nạp chồng (Overload) viết riêng ở Repo để hủy nhanh gọn
			await _unitOfWork.CustomerRefreshTokens.RevokeTokenAsync(refreshTokenValue);
		}

		public async Task<bool> ValidateRefreshTokenAsync(ValidationRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.RefreshToken)) return false;

			var query = new CustomerTokenQuery
			{
				RefreshToken = request.RefreshToken,
				CustomerId = request.CustomerId,
				OnlyActive = true // Ép Repo check: Chưa hủy + Chưa dùng + Còn hạn
			};

			var token = await _unitOfWork.CustomerRefreshTokens.GetByTokenAsync(query);
			return token != null;
		}
	}
}
