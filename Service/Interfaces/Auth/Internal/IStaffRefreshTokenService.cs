using TokenRotationRequest = Shared.DTOs.DTO.Internal.TokenRotationRequest;
using ValidationRequest = Shared.DTOs.DTO.Internal.ValidationRequest;
namespace Service.Interfaces.Auth.Internal
{
	public interface IStaffRefreshTokenService
	{
		Task<StaffRefreshToken> GenerateRefreshTokenAsync(int staffId);
		Task<StaffRefreshToken?> GetTokenAsync(TokenQuery query);

		Task<StaffRefreshToken> ReplaceRefreshTokenAsync(TokenRotationRequest request);

		Task RevokeTokenAsync(string refreshTokenValue);
		Task<bool> ValidateRefreshTokenAsync(ValidationRequest request);
	}
}
