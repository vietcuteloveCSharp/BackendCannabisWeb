using DAL.Models;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
	public interface IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto>
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{
		Task<ApiResponse<PagedResult<TReadDto>>> GetPagedAsync(int page, int size,string? search);
		Task<ApiResponse<TReadDto>> GetByIdAsync(int id);
		Task<ApiResult> CreateAsync(TCreateDto dto);
		Task<ApiResult> UpdateAsync(int id, TUpdateDto dto);
		Task<ApiResult> DeleteAsync(int id);
		Task<ApiResponse<bool>> ExistsAsync(int id);
		

	}
	public interface IBaseService<TEntity, TDto>
	: IBaseService<TEntity, TDto, TDto, TDto> // Ép 3 loại DTO làm 1
	where TEntity : class
	where TDto : class
	{

	}
}
