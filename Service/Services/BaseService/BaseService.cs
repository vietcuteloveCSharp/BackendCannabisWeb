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
		public virtual async Task<ApiResponse<PagedResult<TReadDto>>> GetPagedAsync(QueryParam query)
		{
			Expression<Func<TEntity, bool>>? filter = null;
			// Nếu có search, ta build predicate ở đây
			if (!string.IsNullOrWhiteSpace(query.Search))
			{
				filter = BuildSearchFilter(query.Search);
			}
			// 1. Gọi Repo lấy dữ liệu phân trang thô
			var pagedResult = await _repository.GetPagedAsync(query.Page, query.Size, filter);

			// 2. Map danh sách Entity sang DTO
			var dtos = _mapper.Map<IEnumerable<TReadDto>>(pagedResult.Items);
			//đóng gói vào paged reuslt
			var pagedDto = new PagedResult<TReadDto>(dtos, pagedResult.TotalCount, query.Page, query.Size);

			// 3. Đóng gói vào ApiResponse (Dùng hàm Paged đã viết ở DTO)
			return ApiResponse<PagedResult<TReadDto>>.Ok(pagedDto, "Lấy dữ liệu thành công");
		}
		// 4. XÓA (Hàm DeleteAsync đã có logic IsDeleted trong BaseRepo của bạn)
		public virtual async Task<ApiResult> SoftDeleteAsync(int id)
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

		public virtual async Task<ApiResponse<bool>> ExistsAsync(int id)
		{
			// Gọi hàm AnyAsync đã có sẵn trong BaseRepository của bạn
			var exists = await _repository.AnyAsync(x => EF.Property<int>(x, "Id") == id);

			return ApiResponse<bool>.Ok(exists, exists ? "Đối tượng tồn tại" : "Đối tượng không tồn tại");
		}

		protected virtual Expression<Func<TEntity, bool>>? BuildSearchFilter(string search) => null;

		public virtual Task<ApiResult> DeleteManyAsync(List<int> ids)
		{
			return SoftDeleteManyAsync(ids);
		}
		// get all k có phân trang
		public virtual async Task<ApiResponse<IEnumerable<TReadDto>>> GetAllAsync()
		{
			var entities = await _repository.GetAllAsync();
			var dtos = _mapper.Map<IEnumerable<TReadDto>>(entities);
			return ApiResponse<IEnumerable<TReadDto>>.Ok(dtos, "Lấy tất cả dữ liệu thành công");
		}
		// XÓA NHIỀU (mặc định là SOFT DELETE để an toàn)
		// -> chỉ là wrapper gọi lại SoftDeleteManyAsync
		public virtual async Task<ApiResult> SoftDeleteManyAsync(List<int> ids)
		{
			if (ids == null || !ids.Any())
				return ApiResult.Fail("Danh sách ID không hợp lệ.");
			var affected = await _repository.ExecuteUpdateBatchAsync(
				 x => ids.Contains(EF.Property<int>(x, "Id")) && EF.Property<bool>(x,"IsDeleted")==false,
				 s => s
					.SetProperty(e => EF.Property<bool>(e, "IsDeleted"), true)
					.SetProperty(e => EF.Property<DateTime?>(e, "DeletedAt"), DateTime.UtcNow)
					);
			return affected > 0
				? ApiResult.Ok($"Đã xóa tạm {affected} bản ghi.")
				: ApiResult.Fail("Không có bản ghi nào được cập nhật.");
		}
		// XÓA CỨNG 1 RECORD (XÓA VĨNH VIỄN KHỎI DB)
		// -> dùng ExecuteDeleteBatchAsync để không load entity
		public virtual async Task<ApiResult> HardDeleteAsync(int id)
		{
			var affected = await _repository.ExecuteDeleteBatchAsync(
				x => EF.Property<int>(x, "Id") == id
			);

			return affected > 0
				? ApiResult.Ok("Xóa vĩnh viễn thành công.")
				: ApiResult.Fail("Không tìm thấy dữ liệu để xóa.");
		}
		// XÓA CỨNG NHIỀU RECORD
		// -> xóa trực tiếp DB, KHÔNG qua tracking
		public virtual async Task<ApiResult> HardDeleteManyAsync(List<int> ids)
		{
			if (ids == null || !ids.Any())
				return ApiResult.Fail("Danh sách ID không hợp lệ.");

			var affected = await _repository.ExecuteDeleteBatchAsync(
				x => ids.Contains(EF.Property<int>(x, "Id"))
			);

			return affected > 0
				? ApiResult.Ok($"Đã xóa vĩnh viễn {affected} bản ghi.")
				: ApiResult.Fail("Không có bản ghi nào được xóa.");
		}
		// KHÔI PHỤC 1 RECORD ĐÃ BỊ SOFT DELETE
		// -> set IsDeleted = false, DeletedAt = null
		public virtual async Task<ApiResult> RestoreAsync(int id)
		{
			var affected = await _repository.ExecuteUpdateBatchAsync(
			x => EF.Property<int>(x, "Id") == id
				 && EF.Property<bool>(x, "IsDeleted") == true, // chỉ restore nếu đã bị xóa
				s => s
				.SetProperty(e => EF.Property<bool>(e, "IsDeleted"), false)
				.SetProperty(e => EF.Property<DateTime?>(e, "DeletedAt"), (DateTime?)null)
			);

			return affected > 0
				? ApiResult.Ok("Khôi phục dữ liệu thành công.")
				: ApiResult.Fail("Không tìm thấy dữ liệu đã xóa để khôi phục.");
		}
		// KHÔI PHỤC NHIỀU RECORD
		// -> chỉ restore những record đang IsDeleted = true
		public virtual async Task<ApiResult> RestoreManyAsync(List<int> ids)
		{
			if (ids == null || !ids.Any())
				return ApiResult.Fail("Danh sách ID không hợp lệ.");

			var affected = await _repository.ExecuteUpdateBatchAsync(
				x => ids.Contains(EF.Property<int>(x, "Id"))
					 && EF.Property<bool>(x, "IsDeleted") == true,
				s => s
					.SetProperty(e => EF.Property<bool>(e, "IsDeleted"), false)
					.SetProperty(e => EF.Property<DateTime?>(e, "DeletedAt"), (DateTime?)null)
			);

			return affected > 0
				? ApiResult.Ok($"Đã khôi phục {affected} bản ghi.")
				: ApiResult.Fail("Không có bản ghi nào được khôi phục.");
		}
	}
	public abstract class BaseService<TEntity, TDto>
	: BaseService<TEntity, TDto, TDto, TDto>
	where TEntity : class where TDto : class
	{
		protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
			: base(unitOfWork, mapper) { }
	}
}

