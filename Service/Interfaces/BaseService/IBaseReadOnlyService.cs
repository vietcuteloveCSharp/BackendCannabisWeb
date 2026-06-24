namespace Service.Interfaces.BaseService
{
	public interface IBaseReadOnlyService<TEntity, TReadDto>
		where TEntity : class
		where TReadDto : class	
	{
		Task<ApiResponse<IEnumerable<TReadDto>>> GetAllAsync();
		Task<ApiResponse<PagedResult<TReadDto>>> GetPagedAsync(QueryParam query);
		Task<ApiResponse<TReadDto>> GetByIdAsync(int id);

	}

}
