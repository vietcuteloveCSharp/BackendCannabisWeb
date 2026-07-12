
namespace DAL.Repository.Implementations.Internal
{
	public class StaffRefreshTokenRepository: BaseRepository<StaffRefreshToken>,IStaffRefreshTokenRepository
	{
		
		public StaffRefreshTokenRepository(CannabisAccessoriesDBContext context):base(context)
		{

		}
		// Tìm nhanh theo chuỗi Token thô
		public async Task<StaffRefreshToken?> GetByTokenAsync(string tokenHash)
		{
			return await _context.StaffRefreshTokens
				.FirstOrDefaultAsync(t => t.TokenHash == tokenHash && t.IsRevoked == false);
		}

		// Tìm nâng cao theo Object Query
		public async Task<StaffRefreshToken?> GetByTokenAsync(TokenQuery query)
		{
			//bắt đầu bằng 1 Queryable sạch 
			var dbQuery = _context.StaffRefreshTokens.AsQueryable();
			// 1. Xác thực chéo bắt buộc: Nếu truyền cả 2 thì phải KHỚP ĐỒNG THỜI cả mã và chủ sở hữu
			if (!string.IsNullOrWhiteSpace(query.RefreshToken))
				dbQuery = dbQuery.Where(t => t.TokenHash == query.RefreshToken);

			if (query.StaffId.HasValue)
				dbQuery = dbQuery.Where(t => t.StaffId == query.StaffId.Value);

			// 2. Phân loại điều kiện lọc trạng thái Token

			if (query.OnlyActive)
				dbQuery = dbQuery.Where(t => t.IsRevoked == false && t.IsUsed == false && t.ExpiresAt > DateTime.UtcNow);
			else if (!query.IncludeRevoked)
				// Chỉ lấy những token chưa bị hủy
				dbQuery = dbQuery.Where(t => t.IsRevoked == false);

			return await dbQuery.FirstOrDefaultAsync();
		}

		// Xóa token bằng chuỗi định danh (Soft delete hoặc Hard delete tùy bồ)
		public async Task<bool> DeleteAsync(string tokenHash)
		{
			var token = await GetByTokenAsync(tokenHash);
			if (token == null) return false;

			_context.StaffRefreshTokens.Remove(token); // Hoặc token.IsRevoked = true nếu làm soft delete
			return true;
		}

	}
}
