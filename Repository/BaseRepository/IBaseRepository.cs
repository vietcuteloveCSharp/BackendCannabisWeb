using System.Linq.Expressions;

namespace Repository.BaseRepository
{
	public interface IBaseRepository<T> where T : class
	{
		Task<IEnumerable<T?>> GetAllAsync();
		Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T?>> FindAllAsync(Expression<Func<T, bool>> predicate);
		Task<T?> GetByIdAsync(int id);
		Task<T?> AddAsync(T entity);
		Task<T?> UpdateAsync(int id, T entity);
		Task<bool> DeleteAsync(int id);
	}
}
