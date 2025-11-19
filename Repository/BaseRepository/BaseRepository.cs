using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.BaseRepository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly CannabisAccessorriesDBContext _context;
		protected readonly DbSet<T> _dbSet;
		public BaseRepository(CannabisAccessorriesDBContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}
		//base add method for all repositories
		public async Task<T?> AddAsync(T entity)
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

		public async Task<IEnumerable<T?>> FindAllAsync(Expression<Func<T, bool>> predicate)
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

		public async Task<IEnumerable<T?>> GetAllActiveAsync()
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
		public async Task<IEnumerable<T?>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}
		// base get by id method for all repositories
		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}
		// base update method for all repositories
		public bool Update(T updatedEntity)
		{
			_dbSet.Update(updatedEntity); // mark entity modified
			return true;
		}
	}
}
