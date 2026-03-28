using DAL.Models;
using DTO.Response;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.BaseService
{
	public abstract class BaseService<TEntity, TReadDto, TCreateDto, TUpdateDto> : IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto>
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{
		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IMapper _mapper;
		protected readonly IBaseRepository<TEntity> _repository;
		protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			// Tự động tìm đúng Repository tương ứng với Entity
			_repository = _unitOfWork.Repository<TEntity>();
		}
		//get all
		public virtual async Task<ApiResponse<PagedResult<TReadDto>>> GetPagedAsync(int page, int size, string? search)
		{
			Expression<Func<TEntity, bool>>? filter = null;
			// Nếu có search, ta build predicate ở đây
			if (!string.IsNullOrWhiteSpace(search))
			{
				filter = BuildSearchFilter(search);
			}
			// 1. Gọi Repo lấy dữ liệu phân trang thô
			var pagedResult = await _repository.GetPagedAsync(page, size, filter);

			// 2. Map danh sách Entity sang DTO
			var dtos = _mapper.Map<IEnumerable<TReadDto>>(pagedResult.Items);
			//đóng gói vào paged reuslt
			var pagedDto = new PagedResult<TReadDto>(dtos, pagedResult.TotalCount, page, size);

			// 3. Đóng gói vào ApiResponse (Dùng hàm Paged đã viết ở DTO)
			return ApiResponse<PagedResult<TReadDto>>.Ok(pagedDto,"Lấy dữ liệu thành công");
		}
		// 4. XÓA (Hàm DeleteAsync đã có logic IsDeleted trong BaseRepo của bạn)
		public virtual async Task<ApiResult> DeleteAsync(int id)
		{
			var success = await _repository.DeleteAsync(id);

			if (!success)
				return ApiResult.Fail("Không tìm thấy dữ liệu hoặc dữ liệu đã bị xóa trước đó.");

			var result = await _unitOfWork.SaveChangesAsync();

			return result > 0
				? ApiResult.Ok("Xóa dữ liệu thành công.")
				: ApiResult.Fail("Lỗi hệ thống khi thực hiện xóa.");
		}

		// 1. LẤY CHI TIẾT THEO ID
		public virtual async Task<ApiResponse<TReadDto>> GetByIdAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<TReadDto>.Fail("Không tìm thấy dữ liệu yêu cầu.");

			var dto = _mapper.Map<TReadDto>(entity);
			return ApiResponse<TReadDto>.Ok(dto);
		}

		// 2. TẠO MỚI
		public virtual async Task<ApiResult> CreateAsync(TCreateDto dto)
		{
			// Map từ DTO người dùng gửi lên sang Entity để lưu DB
			var entity = _mapper.Map<TEntity>(dto);

			await _repository.AddAsync(entity);
			var result = await _unitOfWork.SaveChangesAsync();

			return result > 0
				? ApiResult.Ok("Thêm mới thành công.")
				: ApiResult.Fail("Thêm mới thất bại.");
		}


		// 3. CẬP NHẬT
		public virtual async Task<ApiResult> UpdateAsync(int id, TUpdateDto dto)
		{
			var existingEntity = await _repository.GetByIdAsync(id);

			if (existingEntity == null)
				return ApiResult.Fail("Không tìm thấy dữ liệu để cập nhật.");

			// Map đè dữ liệu từ DTO vào Entity đang có (giúp giữ lại các trường không đổi)
			_mapper.Map(dto, existingEntity);

			_repository.Update(existingEntity);
			var result = await _unitOfWork.SaveChangesAsync();

			return result > 0
				? ApiResult.Ok("Cập nhật thành công.")
				: ApiResult.Fail("Không có thay đổi nào được ghi lại.");
		}

		public async Task<ApiResponse<bool>> ExistsAsync(int id)
		{
			// Gọi hàm AnyAsync đã có sẵn trong BaseRepository của bạn
			var exists = await _repository.AnyAsync(x => EF.Property<int>(x, "Id") == id);

			return ApiResponse<bool>.Ok(exists, exists ? "Đối tượng tồn tại" : "Đối tượng không tồn tại");
		}

		protected virtual Expression<Func<TEntity, bool>>? BuildSearchFilter(string search) => null;

	}
	public abstract class BaseService<TEntity, TDto>
	: BaseService<TEntity, TDto, TDto, TDto>
	where TEntity : class where TDto : class
	{
		protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
			: base(unitOfWork, mapper) { }
	}
}

