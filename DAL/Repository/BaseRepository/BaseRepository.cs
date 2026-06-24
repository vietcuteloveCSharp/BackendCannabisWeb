namespace DAL.Repository.BaseRepository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly CannabisAccessoriesDBContext _context;
		protected readonly DbSet<T> _dbSet;
		public BaseRepository(CannabisAccessoriesDBContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}
		//base add method for all repositories
		public async Task<T> AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			return entity;
		}
		// soft delete 
		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity == null) { return false; }
			if (entity is ISoftDelete softDeleteEntity)
			{
				softDeleteEntity.IsDeleted = true;
				softDeleteEntity.DeletedAt = DateTime.UtcNow;
				_dbSet.Update(entity);
			}
			else
			{
				_dbSet.Remove(entity);
			}
			return true;

		}
		// tìm với any
		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		// base get by id method for all repositories
		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}
		// get query
		public IQueryable<T> GetQueryable(bool trackChanges = false)
		{
			// Trả về Queryable để cho phép xây dựng câu lệnh SQL ở tầng Service
			// Sử dụng AsNoTracking() nếu bạn chỉ muốn đọc dữ liệu để tăng hiệu năng
			return trackChanges?  _dbSet.AsQueryable() :_dbSet.AsNoTracking();
		}

		// base update method for all repositories
		public bool Update(T updatedEntity)
		{
			_dbSet.Update(updatedEntity); // mark entity modified
			return true;
		}
		// tìm 1 hoặc  mặc định 
		public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, params Expression<Func<T, object>>[] includes)
		{
			var query = GetQueryable(trackChanges);

			if (includes.Any())
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			return await query.FirstOrDefaultAsync(predicate);
		}

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, bool trackChanges = false, params Expression<Func<T, object>>[] includes)
		{
			var query = GetQueryable(trackChanges);
			if (includes.Any())
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			if (predicate != null) query = query.Where(predicate);
			return await query.ToListAsync();
		}


		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await _dbSet.AddRangeAsync(entities);
			return;
		}

		public bool  UpdateRange(IEnumerable<T> entities)
		{
			_dbSet.UpdateRange(entities);
			return true;
		}

		public async Task<int> ExecuteDeleteBatchAsync(Expression<Func<T, bool>> predicate)
			=> await _dbSet.Where(predicate).ExecuteDeleteAsync();
		

		public async Task<int> ExecuteUpdateBatchAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
		=> await _dbSet.Where(predicate).ExecuteUpdateAsync(updateExpression);
		//phân trang
		public async Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize,Expression<Func<T, bool>>? predicate = null, bool trackChanges = false, params Expression<Func<T, object>>[] includes)
		{
			var query = GetQueryable(trackChanges);
			// 1. Eager Loading (Include)
			if (includes.Any())
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			// 2. Filter (Where)
			if (predicate != null)
			{
				query = query.Where(predicate);
			}
			
			// 3. Đếm tổng số bản ghi TRƯỚC khi phân trang
			var totalCount = await query.CountAsync();
			var items = await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
		}
	}
}
