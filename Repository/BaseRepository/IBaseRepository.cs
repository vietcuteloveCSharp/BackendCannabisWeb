using DAL.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.BaseRepository
{
	public interface IBaseRepository<T> where T : class
	{
		// --- QUERIES (ĐỌC)
		IQueryable<T> GetQueryable(bool trackChanges =false);
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, bool trackChanges = false, params Expression<Func<T, object>>[] includes);
		Task<T?> GetByIdAsync(int id);

		Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, params Expression<Func<T, object>>[] includes);

		// --- COMMANDS (GHI) ---
		Task<T> AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		bool Update(T entity);
		bool UpdateRange(IEnumerable<T> entities);
		Task<bool> DeleteAsync(int id);
		// --- BULK OPERATIONS
		Task<int> ExecuteDeleteBatchAsync(Expression<Func<T, bool>> predicate);
		Task<int> ExecuteUpdateBatchAsync(
			Expression<Func<T, bool>> predicate,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
		//phân trang
		Task<PagedResult<T>> GetPagedAsync(
		int pageNumber,
		int pageSize,
		Expression<Func<T, bool>>? predicate = null,
		bool trackChanges = false,
		params Expression<Func<T, object>>[] includes);
	}
}
