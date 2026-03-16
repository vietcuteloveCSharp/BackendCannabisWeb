using System.Linq.Expressions;

namespace Repository.BaseRepository
{
	public interface IBaseRepository<T> where T : class
	{
		IQueryable<T> GetQueryable();
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllActiveAsync();
		Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
		Task<T?> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		bool Update(T entity);
		Task<bool> DeleteAsync(int id);
	}
}
