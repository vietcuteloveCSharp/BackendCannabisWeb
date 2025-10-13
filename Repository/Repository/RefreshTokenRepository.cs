namespace Repository.Repository
{
	public class RefreshTokenRepository : IRefreshTokenRepository
	{	private readonly CannabisAccessorriesDBContext _context;
		public RefreshTokenRepository(CannabisAccessorriesDBContext context)
		{
			this._context = context;
		}
		// add refresh token
		public async Task AddAsync(RefreshToken refreshToken)
		{
			await _context.RefreshTokens.AddAsync(refreshToken);
		}

		public async Task<bool> ExistsAsync(string refreshToken)
		{
			return await _context.RefreshTokens
				.AnyAsync(t => t.RefreshTokenValue == refreshToken);
		}

		// Get refresh token by token string và includeRevoked thì bỏ
		public async Task<RefreshToken?> GetByTokenAsync(string refreshToken, bool includeRevoked = false)
		{
			var query = _context.RefreshTokens
							 .Include(rt => rt.User)
							 .AsQueryable();
			if (!includeRevoked)
				query = query.Where(rt => !rt.IsRevoked);
			return await query.FirstOrDefaultAsync(rt => rt.RefreshTokenValue == refreshToken);
		}
		// Get list refresh token by userId
		public async Task<List<RefreshToken>> GetByUserIdAsync(int userId, bool onlyActive = true)
		{
			var query = _context.RefreshTokens.Where(rt => rt.UserId == userId);
			if (onlyActive)
			{
				query=query.Where(rt => !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow);
			}
			return await query.ToListAsync();
		}
		// Get latest refresh token by userId
		public async Task<RefreshToken?> GetLatestByUserIdAsync(int userId, bool onlyActive = true)
		{
			var query = _context.RefreshTokens
							   .Where(rt => rt.UserId == userId);

			if (onlyActive)
				query = query.Where(rt => !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow);

			return await query
						 .OrderByDescending(rt => rt.CreatedAt)
						 .FirstOrDefaultAsync();
		}
		// Revoke all tokens of user
		public async Task<int> RevokeAllAsync(int userId)
		{
			var tokens = await _context.RefreshTokens
					.Where(t => t.UserId == userId && !t.IsRevoked)
					.ToListAsync();

			foreach (var token in tokens)
			{
				token.IsRevoked = true;
			}
			return tokens.Count;
		}

		//Delete refresh token
		public async Task<bool> RevokeTokenAsync(string token)
		{
			var existing = await GetByTokenAsync(token);
			if (existing != null)
			{
				existing.IsRevoked = true;
				_context.RefreshTokens.Update(existing);
			}
			return true;
		}

		public  Task UpdateAsync(RefreshToken token)
		{
			_context.RefreshTokens.Update(token);
			return Task.CompletedTask;

		}
		
	}
}
