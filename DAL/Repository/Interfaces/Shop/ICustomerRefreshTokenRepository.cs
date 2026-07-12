
namespace DAL.Repository.Interfaces.Shop
{
	public interface ICustomerRefreshTokenRepository :IBaseRepository<CustomerRefreshToken>
	{
		// 🔥 OVERLOAD: Tìm kiếm nâng cao qua Object Query
		Task<CustomerRefreshToken?> GetByTokenAsync(CustomerTokenQuery query);
		Task<List<CustomerRefreshToken>> GetByCustomerIdAsync(CustomerTokenQuery query);
		Task<CustomerRefreshToken?> GetLatestByCustomerIdAsync(CustomerTokenQuery query);

		// 🔥 OVERLOAD: Xử lý nghiệp vụ nhanh bằng chuỗi Token hoặc Id thay vì truyền cả Entity
		Task<bool> ExistsAsync(string refreshToken);
		Task<bool> RevokeTokenAsync(string token);
		Task<int> RevokeAllAsync(int customerId);
	}
}
