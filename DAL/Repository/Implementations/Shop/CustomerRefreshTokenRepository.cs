
namespace DAL.Repository.Implementations.Shop
{
	public class CustomerRefreshTokenRepository : BaseRepository<CustomerRefreshToken>, ICustomerRefreshTokenRepository
	{

		public CustomerRefreshTokenRepository(CannabisAccessoriesDBContext context) :base(context) { }
		public async Task<CustomerRefreshToken?> GetByTokenAsync(CustomerTokenQuery query)
		{
			var dbQuery = GetQueryable(trackChanges: true); // Bật tracker để chuẩn bị Update trạng thái

			if (!string.IsNullOrWhiteSpace(query.RefreshToken))
				dbQuery = dbQuery.Where(t => t.TokenHash == query.RefreshToken);

			if (query.CustomerId.HasValue)
				dbQuery = dbQuery.Where(t => t.CustomerId == query.CustomerId.Value);

			if (query.OnlyActive)
				dbQuery = dbQuery.Where(t => t.IsRevoked == false && t.IsUsed == false && t.ExpiresAt > DateTime.UtcNow);
			else if (!query.IncludeRevoked)
				dbQuery = dbQuery.Where(t => t.IsRevoked == false);

			return await dbQuery.FirstOrDefaultAsync();
		}

		public async Task<List<CustomerRefreshToken>> GetByCustomerIdAsync(CustomerTokenQuery query)
		{
			var dbQuery = GetQueryable(trackChanges: false).Where(t => t.CustomerId == query.CustomerId);

			if (!query.IncludeRevoked)
				dbQuery = dbQuery.Where(t => t.IsRevoked == false);

			return await dbQuery.ToListAsync();
		}

		public async Task<CustomerRefreshToken?> GetLatestByCustomerIdAsync(CustomerTokenQuery query)
		{
			return await GetQueryable(trackChanges: false)
				.Where(t => t.CustomerId == query.CustomerId && t.IsRevoked == false)
				.OrderByDescending(t => t.CreatedAt)
				.FirstOrDefaultAsync();
		}

		public async Task<bool> ExistsAsync(string refreshToken)
		{
			return await AnyAsync(t => t.TokenHash == refreshToken && t.IsRevoked == false && t.ExpiresAt > DateTime.UtcNow);
		}

		public async Task<bool> RevokeTokenAsync(string token)
		{
			var tokenEntity = await GetQueryable(trackChanges: true)
				.FirstOrDefaultAsync(t => t.TokenHash == token && t.IsRevoked == false);

			if (tokenEntity == null) return false;

			tokenEntity.IsRevoked = true;
			Update(tokenEntity); // Gọi hàm Update đồng bộ của lớp Base
			return true;
		}

		public async Task<int> RevokeAllAsync(int customerId)
		{
			// Sử dụng tính năng ExecuteUpdateBatchAsync cực mạnh từ Base để hủy nhanh không cần nạp lên RAM
			return await ExecuteUpdateBatchAsync(
				t => t.CustomerId == customerId && t.IsRevoked == false,
				u => u.SetProperty(t => t.IsRevoked, true)
			);
		}
	}
}
