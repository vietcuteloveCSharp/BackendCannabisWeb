using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.BaseRepository
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
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity == null) { return false; }
			var property = entity.GetType().GetProperty("IsDeleted");
			if (property != null)
			{
				property.SetValue(entity, true);
				_dbSet.Update(entity);

			}
			else
			{
				_dbSet.Remove(entity);
			}
			return true;

		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
		{
			var query = _dbSet.AsQueryable();
			var property = typeof(T).GetProperty("IsDeleted");

			if (property != null)
				query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);

			return await query.Where(predicate).ToListAsync();
		}

		public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
		{
			var query = _dbSet.AsQueryable();
			var property = typeof(T).GetProperty("IsDeleted");

			if (property != null)
				query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);

			return await query.FirstOrDefaultAsync(predicate);
		}

		public async Task<IEnumerable<T>> GetAllActiveAsync()
		{
			var property = typeof(T).GetProperty("IsDeleted");
			if (property != null)
			{
				return await _dbSet
					.Where(e => EF.Property<bool>(e, "IsDeleted") == false)
					.ToListAsync();
			}
			return await _dbSet.ToListAsync();
		}

		// base get all method for all repositories
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		// base get by id method for all repositories
		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public IQueryable<T> GetQueryable()
		{
			// Trả về Queryable để cho phép xây dựng câu lệnh SQL ở tầng Service
			// Sử dụng AsNoTracking() nếu bạn chỉ muốn đọc dữ liệu để tăng hiệu năng
			return _dbSet.AsQueryable();
		}

		// base update method for all repositories
		public bool Update(T updatedEntity)
		{
			_dbSet.Update(updatedEntity); // mark entity modified
			return true;
		}

		public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;

			// Load các bảng liên quan nếu có
			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.FirstOrDefaultAsync(predicate);
		}

		public async Task<T?> DeleteRangeAsync(Expression<Func<T, bool>>[] ids)
		{
			var keyName = _dbSet
		}
	}
}
