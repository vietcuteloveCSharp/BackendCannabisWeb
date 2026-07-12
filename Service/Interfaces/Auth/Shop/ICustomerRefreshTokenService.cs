using TokenRotationRequest = Shared.DTOs.DTO.Shop.TokenRotationRequest;
using ValidationRequest = Shared.DTOs.DTO.Shop.ValidationRequest;

namespace Service.Interfaces.Auth.Shop
{
	public interface ICustomerRefreshTokenService
	{
		Task<CustomerRefreshToken> GenerateRefreshTokenAsync(int customerId);
		Task<CustomerRefreshToken?> GetTokenAsync(CustomerTokenQuery query);
		Task<CustomerRefreshToken> ReplaceRefreshTokenAsync(TokenRotationRequest request);
		Task RevokeTokenAsync(string refreshTokenValue);
		Task<bool> ValidateRefreshTokenAsync(ValidationRequest request);
	}
}
