

namespace Service.Interfaces.BaseService
{
	public interface IBaseCRUDService<TEntity, TReadDto, TCreateDto, TUpdateDto> : IBaseReadOnlyService<TEntity, TReadDto>
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{
		Task<ApiResult> CreateAsync(TCreateDto dto);
		Task<ApiResult> UpdateAsync(int id, TUpdateDto dto);
		Task<ApiResult> SoftDeleteAsync(int id);
		Task<ApiResult> DeleteManyAsync(List<int> ids);
		Task<ApiResult> HardDeleteAsync(int id);
		Task<ApiResult> HardDeleteManyAsync(List<int> ids);
		Task<ApiResult> RestoreAsync(int id);
		Task<ApiResult> RestoreManyAsync(List<int> ids);
		Task<ApiResponse<bool>> ExistsAsync(int id);
	}
}
